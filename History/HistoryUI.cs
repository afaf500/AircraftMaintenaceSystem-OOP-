using System;
using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // UI: shows history screens. The filtering is done by HistoryService.
    class HistoryUI
    {
        private HistoryService service;

        public HistoryUI(HistoryService historyService)
        {
            service = historyService;
        }

        public void ViewAdminHistory()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("           ADMIN ACTION HISTORY             ");
            Console.WriteLine("============================================");
            List<HistoryModel> list = service.GetAdminHistory();
            if (list.Count == 0)
            {
                Console.WriteLine("No admin history found.");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine(list[i].Entry);
            }
            ConsoleHelper.PressAnyKey();
        }

        public void ViewTechHistory(string username)
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("            YOUR ACTION HISTORY             ");
            Console.WriteLine("============================================");
            List<HistoryModel> list = service.GetTechHistory(username);
            if (list.Count == 0)
            {
                Console.WriteLine("No history found.");
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine(list[i].Entry);
            }
            ConsoleHelper.PressAnyKey();
        }
    }
}
