namespace AircraftMaintenanceSystem
{
    // only holds the data of ONE aircraft. No logic.
    class AircraftModel
    {
        public string ID { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public AircraftModel(string id, string model, int year)
        {
            ID = id;
            Model = model;
            Year = year;
        }
    }
}
