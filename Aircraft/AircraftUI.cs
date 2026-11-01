using System;
using System.Collections.Generic;

namespace AircraftMaintenanceSystem
{
    // UI: only prints to the screen and reads from the keyboard.
    // All decisions are made by AircraftService.
    class AircraftUI
    {
        private AircraftService service;

        public AircraftUI(AircraftService aircraftService)
        {
            service = aircraftService;
        }

        public void AddAircraft()
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                if (service.IsFull())
                {
                    Console.WriteLine("\n(!) Database is full.");
                    break;
                }
                Console.Write("\nEnter Aircraft ID    : ");
                string id = ConsoleHelper.ReadString();
                if (service.Exists(id))
                {
                    Console.WriteLine("(!) Aircraft ID already exists.");
                }
                else
                {
                    Console.Write("Enter Aircraft Model : ");
                    string model = ConsoleHelper.ReadString();
                    Console.Write("Enter Aircraft Year  : ");
                    int year = ConsoleHelper.ReadInt();
                    service.Add(id, model, year);
                    Console.WriteLine("\nAircraft added successfully.");
                }
                Console.Write("\nAdd another? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void ViewAircraft()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("          ALL AIRCRAFT RECORDS              ");
            Console.WriteLine("============================================");
            if (service.Count() == 0)
            {
                Console.WriteLine("No aircraft records found.");
            }
            else
            {
                List<AircraftModel> list = service.GetAll();
                Console.WriteLine("ID\t\tMODEL\t\t\tYEAR");
                Console.WriteLine("--------------------------------------------");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine(list[i].ID + "\t\t" + list[i].Model + "\t\t" + list[i].Year);
            }
            ConsoleHelper.PressAnyKey();
        }

        public void UpdateAircraft()
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                Console.Write("\nEnter Aircraft ID to update: ");
                string id = ConsoleHelper.ReadString();
                if (service.Exists(id))
                {
                    Console.Write("Enter new Model : ");
                    string model = ConsoleHelper.ReadString();
                    Console.Write("Enter new Year  : ");
                    int year = ConsoleHelper.ReadInt();
                    service.Update(id, model, year);
                    Console.WriteLine("\nAircraft updated successfully.");
                }
                else
                {
                    Console.WriteLine("Aircraft ID not found.");
                }
                Console.Write("\nUpdate another? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void DeleteAircraft()
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                Console.Write("\nEnter Aircraft ID to delete: ");
                string id = ConsoleHelper.ReadString();
                if (service.Delete(id))
                    Console.WriteLine("\nAircraft deleted successfully.");
                else
                    Console.WriteLine("Aircraft ID not found.");
                Console.Write("\nDelete another? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void SearchAircraft()
        {
            char more = 'y';
            while (more == 'y' || more == 'Y')
            {
                Console.Write("\nEnter Aircraft ID to search: ");
                string id = ConsoleHelper.ReadString();
                AircraftModel a = service.Find(id);
                if (a != null)
                {
                    Console.WriteLine("Model : " + a.Model);
                    Console.WriteLine("Year  : " + a.Year);
                }
                else
                {
                    Console.WriteLine("Aircraft ID not found.");
                }
                Console.Write("\nSearch another? (y/n): ");
                more = ConsoleHelper.ReadChar();
            }
            ConsoleHelper.PressAnyKey();
        }

        public void SortAircraft()
        {
            if (service.Count() == 0)
            {
                Console.WriteLine("\nNo aircraft to sort.");
            }
            else
            {
                service.Sort();
                List<AircraftModel> list = service.GetAll();
                Console.WriteLine("\n-------- Sorted Aircraft --------");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine("ID: " + list[i].ID + " | Model: " + list[i].Model + " | Year: " + list[i].Year);
            }
            ConsoleHelper.PressAnyKey();
        }

        // Technician view (ID and Model only)
        public void TechViewAircraft()
        {
            Console.Clear();
            Console.WriteLine("============================================");
            Console.WriteLine("              AIRCRAFT LIST                 ");
            Console.WriteLine("============================================");
            if (service.Count() == 0)
            {
                Console.WriteLine("No aircraft records found.");
            }
            else
            {
                List<AircraftModel> list = service.GetAll();
                Console.WriteLine("ID\t\tMODEL");
                Console.WriteLine("--------------------------------------------");
                for (int i = 0; i < list.Count; i++)
                    Console.WriteLine(list[i].ID + "\t\t" + list[i].Model);
            }
            ConsoleHelper.PressAnyKey();
        }
    }
}
