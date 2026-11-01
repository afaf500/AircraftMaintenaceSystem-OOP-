using System;

namespace AircraftMaintenanceSystem
{
    // UI: login screen + Admin menu + Technician menu.
    // The menus call the UI classes of the other entities (Aircraft, Task, History).
    class UserUI
    {
        private UserService service;
        private AircraftUI aircraftUI;
        private TaskUI taskUI;
        private HistoryUI historyUI;

        public UserUI(UserService userService, AircraftUI aircraftUi, TaskUI taskUi, HistoryUI historyUi)
        {
            service = userService;
            aircraftUI = aircraftUi;
            taskUI = taskUi;
            historyUI = historyUi;
        }

        // Returns the logged in user, or null if login failed
        public UserModel Login()
        {
            Console.WriteLine("============================================");
            Console.WriteLine("                  LOGIN                     ");
            Console.WriteLine("============================================");
            Console.Write("Username : ");
            string username = ConsoleHelper.ReadString();
            Console.Write("Password : ");
            string password = ConsoleHelper.ReadString();

            UserModel user = service.Login(username, password);

            if (user == null)
            {
                Console.WriteLine("\nInvalid username or password.");
                ConsoleHelper.WaitForKey();
                return null;
            }

            if (user.Role == 1)
            {
                Console.WriteLine("\nLogin successful. Welcome, Admin!");
                ConsoleHelper.PressAnyKey();
            }
            else
            {
                Console.WriteLine("\nLogin successful. Welcome, " + user.Username + "!");
            }
            return user;
        }

        // ==================== ADMIN MENU ====================

        public void AdminMenu(string username)
        {
            int choice = 0;
            while (choice != 10)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("                ADMIN MENU                  ");
                Console.WriteLine("============================================");
                Console.WriteLine(" 1.  Add Aircraft");
                Console.WriteLine(" 2.  View Aircraft");
                Console.WriteLine(" 3.  Update Aircraft");
                Console.WriteLine(" 4.  Delete Aircraft");
                Console.WriteLine(" 5.  Search Aircraft");
                Console.WriteLine(" 6.  Sort Aircraft");
                Console.WriteLine(" 7.  View All Tasks");
                Console.WriteLine(" 8.  Delete Task");
                Console.WriteLine(" 9.  View History");
                Console.WriteLine(" 10. Logout");
                Console.WriteLine("============================================");
                Console.Write("Enter choice: ");

                if (!int.TryParse(ConsoleHelper.ReadString(), out choice))
                {
                    choice = 0;
                    continue;
                }

                if (choice == 1)
                    aircraftUI.AddAircraft();
                else if (choice == 2)
                    aircraftUI.ViewAircraft();
                else if (choice == 3)
                    aircraftUI.UpdateAircraft();
                else if (choice == 4)
                    aircraftUI.DeleteAircraft();
                else if (choice == 5)
                    aircraftUI.SearchAircraft();
                else if (choice == 6)
                    aircraftUI.SortAircraft();
                else if (choice == 7)
                    taskUI.ViewAllTasks();
                else if (choice == 8)
                    taskUI.DeleteTask();
                else if (choice == 9)
                    historyUI.ViewAdminHistory();
                else if (choice == 10)
                {
                    Console.Clear();
                    Console.WriteLine("============================================");
                    Console.WriteLine("       Thank you, Admin. Logging out...     ");
                    Console.WriteLine("============================================");
                    ConsoleHelper.WaitForKey();
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    ConsoleHelper.WaitForKey();
                }
            }
        }

        // ==================== TECHNICIAN MENU ====================

        public void TechnicianMenu(string username)
        {
            int choice = 0;
            while (choice != 10)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("            TECHNICIAN MENU                 ");
                Console.WriteLine("============================================");
                Console.WriteLine(" 1.  View Aircraft");
                Console.WriteLine(" 2.  Add Task");
                Console.WriteLine(" 3.  View My Tasks");
                Console.WriteLine(" 4.  Update Task");
                Console.WriteLine(" 5.  Complete Task");
                Console.WriteLine(" 6.  Search Task");
                Console.WriteLine(" 7.  Sort Tasks");
                Console.WriteLine(" 8.  View Pending Tasks");
                Console.WriteLine(" 9.  View My History");
                Console.WriteLine(" 10. Logout");
                Console.WriteLine("============================================");
                Console.Write("Enter choice: ");

                if (!int.TryParse(ConsoleHelper.ReadString(), out choice))
                {
                    choice = 0;
                    continue;
                }

                if (choice == 1)
                    aircraftUI.TechViewAircraft();
                else if (choice == 2)
                    taskUI.TechAddTask(username);
                else if (choice == 3)
                    taskUI.TechViewMyTasks(username);
                else if (choice == 4)
                    taskUI.TechUpdateTask(username);
                else if (choice == 5)
                    taskUI.TechCompleteTask(username);
                else if (choice == 6)
                    taskUI.TechSearchTask(username);
                else if (choice == 7)
                    taskUI.TechSortTasks(username);
                else if (choice == 8)
                    taskUI.TechViewPendingTasks(username);
                else if (choice == 9)
                    historyUI.ViewTechHistory(username);
                else if (choice == 10)
                {
                    Console.Clear();
                    Console.WriteLine("============================================");
                    Console.WriteLine("  Thank you, " + username + ". Logging out...");
                    Console.WriteLine("============================================");
                    ConsoleHelper.WaitForKey();
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please try again.");
                    ConsoleHelper.WaitForKey();
                }
            }
        }
    }
}
