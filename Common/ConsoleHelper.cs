using System;

namespace AircraftMaintenanceSystem
{
    // Small shared helper used by every UI class.
    // It replaces cin >> and getch() from the C++ program.
    static class ConsoleHelper
    {
        public static string ReadString()
        {
            string s = Console.ReadLine();
            if (s == null)
                return "";
            return s.Trim();             // removes spaces from 
        }

        public static int ReadInt()
        {
            int value = 0;
            int.TryParse(ReadString(), out value);
            return value;
        }

        public static char ReadChar()   // for y/n question
        {
            string s = ReadString();
            if (s.Length == 0)
                return 'n';
            return s[0];
        }

        public static void PressAnyKey()
        {
            Console.Write("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        public static void WaitForKey()
        {
            Console.ReadKey(true);
        }
    }
}
