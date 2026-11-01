using System.Collections.Generic;
using System.IO;

namespace AircraftMaintenanceSystem
{
    // REPOSITORY: keeps the list of tasks in memory and reads/writes tasks.txt
    class TaskRepository
    {
        private const string FILENAME = "tasks.txt";
        public List<TaskModel> Tasks;

        public TaskRepository()
        {
            Tasks = new List<TaskModel>();
            LoadFromFile();
        }

        public void SaveToFile()
        {
            StreamWriter writer = new StreamWriter(FILENAME, false);
            for (int i = 0; i < Tasks.Count; i++)
                writer.WriteLine(Tasks[i].TaskID + " " + Tasks[i].AircraftID + " " + Tasks[i].Status + " " + Tasks[i].Owner);
            writer.Close();
        }

        public void LoadFromFile()
        {
            if (!File.Exists(FILENAME))
                return;
            StreamReader reader = new StreamReader(FILENAME);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(' ');
                if (parts.Length < 4)
                    continue;
                Tasks.Add(new TaskModel(parts[0], parts[1], parts[2], parts[3]));
                if (Tasks.Count >= 50)
                    break;
            }
            reader.Close();
        }
    }
}
