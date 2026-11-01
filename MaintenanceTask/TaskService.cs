using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // SERVICE: business logic for tasks.
    // Uses AircraftService to check that the aircraft exists (aircraft validation)
    // and HistoryService to log actions.
    class TaskService
    {
        private TaskRepository repo;
        private AircraftService aircraftService;
        private HistoryService history;

        public TaskService(TaskRepository repository, AircraftService aircraftSvc, HistoryService historyService)
        {
            repo = repository;
            aircraftService = aircraftSvc;
            history = historyService;
        }

        public List<TaskModel> GetAll()
        {
            return repo.Tasks;
        }

        public int Count()
        {
            return repo.Tasks.Count;
        }

        public bool IsFull()
        {
            return repo.Tasks.Count >= 50;
        }

        // Technician search: task must belong to this user
        public int FindIndex(string id, string user)
        {
            for (int i = 0; i < repo.Tasks.Count; i++)
                if (repo.Tasks[i].TaskID == id && repo.Tasks[i].Owner == user)
                    return i;
            return -1;
        }

        // Admin search: any owner
        public int FindIndexAdmin(string id)
        {
            for (int i = 0; i < repo.Tasks.Count; i++)
                if (repo.Tasks[i].TaskID == id)
                    return i;
            return -1;
        }

        // Returns the task, or null if not found / not owned by the user
        public TaskModel Find(string id, string user)
        {
            int index = FindIndex(id, user);
            if (index == -1)
                return null;
            return repo.Tasks[index];
        }

        // Returns false if the task list is full or the aircraft does not exist
        public bool AddTask(string taskId, string aircraftId, string username)
        {
            if (IsFull())
                return false;
            if (!aircraftService.Exists(aircraftId))
                return false;
            repo.Tasks.Add(new TaskModel(taskId, aircraftId, "Pending", username));
            repo.SaveToFile();
            history.Log("TECH:" + username + " Added Task");
            return true;
        }

        public bool UpdateStatus(string id, string username, string newStatus)
        {
            int index = FindIndex(id, username);
            if (index == -1)
                return false;
            repo.Tasks[index].Status = newStatus;
            repo.SaveToFile();
            history.Log("TECH:" + username + " Updated Task " + id);
            return true;
        }

        public bool CompleteTask(string id, string username)
        {
            int index = FindIndex(id, username);
            if (index == -1)
                return false;
            repo.Tasks[index].Status = "Completed";
            repo.SaveToFile();
            history.Log("TECH:" + username + " Completed Task " + id);
            return true;
        }

        // Admin only
        public bool DeleteTask(string id)
        {
            int index = FindIndexAdmin(id);
            if (index == -1)
                return false;
            repo.Tasks.RemoveAt(index);
            repo.SaveToFile();
            history.Log("ADMIN: Task Deleted");
            return true;
        }

        // Bubble sort by Task ID (sorts the whole list, like the C++ version)
        public void SortTasks(string username)
        {
            int count = repo.Tasks.Count;
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (string.CompareOrdinal(repo.Tasks[j].TaskID, repo.Tasks[j + 1].TaskID) > 0)
                    {
                        TaskModel temp = repo.Tasks[j];
                        repo.Tasks[j] = repo.Tasks[j + 1];
                        repo.Tasks[j + 1] = temp;
                    }
                }
            }
            repo.SaveToFile();
            history.Log("TECH:" + username + " Sorted Tasks");
        }

        public List<TaskModel> GetTasksByOwner(string username)
        {
            List<TaskModel> result = new List<TaskModel>();
            for (int i = 0; i < repo.Tasks.Count; i++)
                if (repo.Tasks[i].Owner == username)
                    result.Add(repo.Tasks[i]);
            return result;
        }

        public List<TaskModel> GetPendingByOwner(string username)
        {
            List<TaskModel> result = new List<TaskModel>();
            for (int i = 0; i < repo.Tasks.Count; i++)
                if (repo.Tasks[i].Owner == username && repo.Tasks[i].Status == "Pending")
                    result.Add(repo.Tasks[i]);
            return result;
        }
    }
}
