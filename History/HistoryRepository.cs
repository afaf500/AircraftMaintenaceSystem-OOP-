using System.Collections.Generic;
using System.IO;

namespace AircraftMaintenanceSystem
{
    // REPOSITORY: keeps the history in memory and reads/appends history.txt
    class HistoryRepository
    {
        private const string FILENAME = "history.txt";
        public List<HistoryModel> Entries;

        public HistoryRepository()
        {
            Entries = new List<HistoryModel>();
            LoadFromFile();
        }

        public void AppendToFile(string entry)
        {
            StreamWriter writer = new StreamWriter(FILENAME, true);
            writer.WriteLine(entry);
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
                Entries.Add(new HistoryModel(line));
                if (Entries.Count >= 200)
                    break;
            }
            reader.Close();
        }
    }
}
