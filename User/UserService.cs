namespace AircraftMaintenanceSystem
{
    // SERVICE: login validation
    class UserService
    {
        private UserRepository repo;

        public UserService(UserRepository repository)
        {
            repo = repository;
        }

        // Returns the matching user, or null if username/password is wrong
        public UserModel Login(string username, string password)
        {
            for (int i = 0; i < repo.Users.Count; i++)
            {
                if (repo.Users[i].Username == username && repo.Users[i].Password == password)
                    return repo.Users[i];
            }
            return null;
        }
    }
}
