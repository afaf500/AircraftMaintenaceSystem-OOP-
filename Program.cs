using System;

namespace AircraftMaintenanceSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1) Create Repositories (they load the text files automatically)
            HistoryRepository historyRepo = new HistoryRepository();
            AircraftRepository aircraftRepo = new AircraftRepository();
            TaskRepository taskRepo = new TaskRepository();
            UserRepository userRepo = new UserRepository();

            // 2) Create Services (each one receives the objects it needs)
            HistoryService historyService = new HistoryService(historyRepo);
            AircraftService aircraftService = new AircraftService(aircraftRepo, historyService);
            TaskService taskService = new TaskService(taskRepo, aircraftService, historyService);
            UserService userService = new UserService(userRepo);

            // 3) Create UIs (each one receives its Service)
            HistoryUI historyUI = new HistoryUI(historyService);
            AircraftUI aircraftUI = new AircraftUI(aircraftService);
            TaskUI taskUI = new TaskUI(taskService);
            UserUI userUI = new UserUI(userService, aircraftUI, taskUI, historyUI);

            // 4) Start the program
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("   AIRCRAFT MAINTENANCE TRACKING SYSTEM     ");
            Console.WriteLine("============================================");

            while (true)
            {
                UserModel user = userUI.Login();
                if (user == null)
                    continue;

                ConsoleHelper.WaitForKey();

                if (user.Role == 1)
                    userUI.AdminMenu(user.Username);
                else if (user.Role == 2)
                    userUI.TechnicianMenu(user.Username);
            }
        }
    }
}
