using System.Collections.Generic;
using System.IO;

namespace AircraftMaintenanceSystem
{
    // REPOSITORY:keeps the list of aircraft in memory and reads/writes aircraft.txt
    class AircraftRepository
    {
        private const string FILENAME = "aircraft.txt";
        public List<AircraftModel> Aircrafts;

        public AircraftRepository()
        {
            Aircrafts = new List<AircraftModel>();
            LoadFromFile();
        }

        public void SaveToFile()
        {
            StreamWriter writer = new StreamWriter(FILENAME, false);
            for (int i = 0; i < Aircrafts.Count; i++)
                writer.WriteLine(Aircrafts[i].ID + " " + Aircrafts[i].Model + " " + Aircrafts[i].Year);
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
                if (parts.Length < 3)
                    continue;
                AircraftModel a = new AircraftModel(parts[0], parts[1], int.Parse(parts[2]));
                Aircrafts.Add(a);
                if (Aircrafts.Count >= 50)
                    break;
            }
            reader.Close();
        }
    }
}
