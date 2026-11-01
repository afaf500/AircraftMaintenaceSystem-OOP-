using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // SERVICE: business logic (validation, add, update, delete, search, sort)
    // Talks to the Repository for data and to HistoryService to log actions.
    class AircraftService
    {
        private AircraftRepository repo;
        private HistoryService history;

        public AircraftService(AircraftRepository repository, HistoryService historyService)
        {
            repo = repository;
            history = historyService;
        }

        public List<AircraftModel> GetAll()
        {
            return repo.Aircrafts;
        }

        public int Count()
        {
            return repo.Aircrafts.Count;
        }

        public bool IsFull()
        {
            return repo.Aircrafts.Count >= 50;
        }

        public bool Exists(string id)
        {
            return FindIndex(id) != -1;
        }

        public int FindIndex(string id)
        {
            for (int i = 0; i < repo.Aircrafts.Count; i++)
                if (repo.Aircrafts[i].ID == id)
                    return i;
            return -1;
        }

        // Returns the aircraft, or null if not found
        public AircraftModel Find(string id)
        {
            int index = FindIndex(id);
            if (index == -1)
                return null;
            return repo.Aircrafts[index];
        }

        public bool Add(string id, string model, int year)
        {
            if (IsFull() || Exists(id))
                return false;
            repo.Aircrafts.Add(new AircraftModel(id, model, year));
            repo.SaveToFile();
            history.Log("ADMIN: Aircraft Added");
            return true;
        }

        public bool Update(string id, string model, int year)
        {
            int index = FindIndex(id);
            if (index == -1)
                return false;
            repo.Aircrafts[index].Model = model;
            repo.Aircrafts[index].Year = year;
            repo.SaveToFile();
            history.Log("ADMIN: Aircraft Updated");
            return true;
        }

        public bool Delete(string id)
        {
            int index = FindIndex(id);
            if (index == -1)
                return false;
            repo.Aircrafts.RemoveAt(index);
            repo.SaveToFile();
            history.Log("ADMIN: Aircraft Deleted");
            return true;
        }

        // Bubble sort by Aircraft ID
        public void Sort()
        {
            int count = repo.Aircrafts.Count;
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - i - 1; j++)
                {
                    if (string.CompareOrdinal(repo.Aircrafts[j].ID, repo.Aircrafts[j + 1].ID) > 0)
                    {
                        AircraftModel temp = repo.Aircrafts[j];
                        repo.Aircrafts[j] = repo.Aircrafts[j + 1];
                        repo.Aircrafts[j + 1] = temp;
                    }
                }
            }
            repo.SaveToFile();
            history.Log("ADMIN: Aircraft Sorted");
        }
    }
}
