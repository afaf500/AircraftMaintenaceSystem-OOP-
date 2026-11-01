using System;
using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // UI: all task screens (Admin and Technician). Decisions are made by TaskService.
    class TaskUI
    {
        private TaskService service;

        public TaskUI(TaskService taskService)
        {
            service = taskService;
        }

        // ---------- ADMIN ----------

        public void ViewAllTasks()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("         ALL MAINTENANCE TASKS              ");
            Console.WriteLine("============================================");
            if (service.Count() == 0)
            {
                Console.WriteLine("No task records found.");
            }
            else
            {
                List<TaskModel> list = service.GetAll();
                Console.WriteLine("TASK ID\t\tAIRCRAFT\tSTATUS\t\tTECHNICIAN");
                Console.WriteLine("------------------------------------------------------------");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine(list[i].TaskID + "\t\t" + list[i].AircraftID + "\t\t" + list[i].Status + "\t\t" + list[i].Owner);
            }
            ConsoleHelper.PressAnyKey();
        }

        public void DeleteTask()
        {
            Console.Write("\nEnter Task ID to delete: ");
            string id = ConsoleHelper.ReadString();
            if (service.DeleteTask(id))
                Console.WriteLine("\nTask deleted successfully.");
            else
                Console.WriteLine("Task ID not found.");
            ConsoleHelper.PressAnyKey();
        }

        // ---------- TECHNICIAN ----------

        public void TechAddTask(string username)
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                if (service.IsFull())
                {
                    Console.WriteLine("\n(!) Task database is full.");
                    break;
                }
                Console.Write("\nEnter Task ID : ");
                string taskId = ConsoleHelper.ReadString();
                Console.Write("Enter Aircraft ID: ");
                string aircraftId = ConsoleHelper.ReadString();
                if (service.AddTask(taskId, aircraftId, username))
                    Console.WriteLine("\nTask added successfully.");
                else
                    Console.WriteLine("(!) Aircraft ID does not exist.");
                Console.Write("\nAdd another task? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void TechViewMyTasks(string username)
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("                MY TASKS                    ");
            Console.WriteLine("============================================");
            List<TaskModel> list = service.GetTasksByOwner(username);
            if (list.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("Task ID: " + list[i].TaskID
                        + " | Aircraft: " + list[i].AircraftID
                        + " | Status: " + list[i].Status);
            }
            ConsoleHelper.PressAnyKey();
        }

        public void TechUpdateTask(string username)
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                Console.Write("\nEnter Task ID to update: ");
                string id = ConsoleHelper.ReadString();
                if (service.Find(id, username) != null)
                {
                    Console.Write("Enter new Status (Pending / InProgress / Completed): ");
                    string status = ConsoleHelper.ReadString();
                    service.UpdateStatus(id, username, status);
                    Console.WriteLine("\nTask updated successfully.");
                }
                else
                {
                    Console.WriteLine("Task ID not found or does not belong to you.");
                }
                Console.Write("\nUpdate another? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void TechCompleteTask(string username)
        {
            Console.Write("\nEnter Task ID to mark as Completed: ");
            string id = ConsoleHelper.ReadString();
            if (service.CompleteTask(id, username))
                Console.WriteLine("\nTask marked as Completed.");
            else
                Console.WriteLine("Task ID not found or does not belong to you.");
            ConsoleHelper.PressAnyKey();
        }

        public void TechSearchTask(string username)
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                Console.Write("\nEnter Task ID to search: ");
                string id = ConsoleHelper.ReadString();
                TaskModel t = service.Find(id, username);
                if (t != null)
                {
                    Console.WriteLine("Task ID  : " + t.TaskID);
                    Console.WriteLine("Aircraft : " + t.AircraftID);
                    Console.WriteLine("Status   : " + t.Status);
                }
                else
                {
                    Console.WriteLine("Task ID not found.");
                }
                Console.Write("\nSearch another? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void TechSortTasks(string username)
        {
            if (service.Count() == 0)
            {
                Console.WriteLine("\nNo tasks to sort.");
            }
            else
            {
                service.SortTasks(username);
                List<TaskModel> list = service.GetTasksByOwner(username);
                Console.WriteLine("\n-------- Your Sorted Tasks --------");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("Task ID: " + list[i].TaskID + " | Status: " + list[i].Status);
            }
            ConsoleHelper.PressAnyKey();
        }

        public void TechViewPendingTasks(string username)
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("            MY PENDING TASKS                ");
            Console.WriteLine("============================================");
            List<TaskModel> list = service.GetPendingByOwner(username);
            if (list.Count == 0)
            {
                Console.WriteLine("No pending tasks found.");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("Task ID: " + list[i].TaskID + " | Aircraft: " + list[i].AircraftID);
            }
            ConsoleHelper.PressAnyKey();
        }
    }
}
