using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // SERVICE: logs new actions and filters history for Admin / Technician.
    // AircraftService and TaskService call Log() whenever something changes.
    class HistoryService
    {
        private HistoryRepository repo;

        public HistoryService(HistoryRepository repository)
        {
            repo = repository;
        }

        public void Log(string entry)
        {
            if (repo.Entries.Count < 200)
                repo.Entries.Add(new HistoryModel(entry));
            repo.AppendToFile(entry);
        }

        // All lines that start with "ADMIN"
        public List<HistoryModel> GetAdminHistory()
        {
            List<HistoryModel> result = new List<HistoryModel>();
            for (int i = 0; i < repo.Entries.Count; i++)
                if (StartsWithText(repo.Entries[i].Entry, "ADMIN"))
                    result.Add(repo.Entries[i]);
            return result;
        }

        // All lines that start with "TECH:" + username
        public List<HistoryModel> GetTechHistory(string username)
        {
            string searchKey = "TECH:" + username;
            List<HistoryModel> result = new List<HistoryModel>();
            for (int i = 0; i < repo.Entries.Count; i++)
                if (StartsWithText(repo.Entries[i].Entry, searchKey))
                    result.Add(repo.Entries[i]);
            return result;
        }

        // Checks the first characters one by one (same idea as the original code)
        private bool StartsWithText(string line, string key)
        {
            for (int j = 0; j < key.Length; j++)
            {
                if (j >= line.Length || line[j] != key[j])
                    return false;
            }
            return true;
        }
    }
}
