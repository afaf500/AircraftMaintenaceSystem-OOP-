using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // REPOSITORY: stores the users.
    // Same as the original program, the login credentials are fixed in the code (no file).
    class UserRepository
    {
        public List<UserModel> Users;

        public UserRepository()
        {
            Users = new List<UserModel>();
            Users.Add(new UserModel("admin", "123", 1));
            Users.Add(new UserModel("tech1", "111", 2));
            Users.Add(new UserModel("tech2", "222", 2));
        }
    }
}
