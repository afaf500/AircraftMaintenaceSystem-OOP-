namespace AircraftMaintenanceSystem
{
    // MODEL: one user account. Role: 1 = Admin, 2 = Technician
    class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }

        public UserModel(string username, string password, int role)
        {
            Username = username;
            Password = password;
            Role = role;
        }
    }
}
