namespace AircraftMaintenanceSystem
{
    // MODEL: data of ONE maintenance task. Owner = technician who created it.
    class TaskModel
    {
        public string TaskID { get; set; }
        public string AircraftID { get; set; }
        public string Status { get; set; }
        public string Owner { get; set; }

        public TaskModel(string taskId, string aircraftId, string status, string owner)
        {
            TaskID = taskId;
            AircraftID = aircraftId;
            Status = status;
            Owner = owner;
        }
    }
}
