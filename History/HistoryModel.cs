namespace AircraftMaintenanceSystem
{
    // MODEL: one line of history, e.g. "ADMIN: Aircraft Added" or "TECH:tech1 Added Task"
    class HistoryModel
    {
        public string Entry { get; set; }

        public HistoryModel(string entry)
        {
            Entry = entry;
        }
    }
}
