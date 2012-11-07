using System;
using System.Collections.Generic;
using System.Linq;

namespace Strategy
{
    class Program
    {
        private static readonly string[] ValidCountries = new[] { "NO", "US" };
        private static readonly Dictionary<int, Action> DayInfoNo = new Dictionary<int, Action>
                                    {
                                        { 1, () => Console.WriteLine("Mandag er den verste dagen i uken! Fire dager igjen til det er helg.") },
                                        { 2, () => Console.WriteLine("Tirsdag. Tre dager igjen til helg.") },
                                        { 3, () => Console.WriteLine("Onsdag... Lillelørdag!") },
                                        { 4, () => Console.WriteLine("Torsdag. Nærmer oss helgen nå!") },
                                        { 5, () => Console.WriteLine("Fredag!! Party on!") },
                                        { 6, () => Console.WriteLine("Lørdag. Klar for mer fest!") },
                                        { 7, () => Console.WriteLine("Søndag. Sliten nå...") }
                                    };
        private static readonly Dictionary<int, Action> DayInfoUs = new Dictionary<int, Action>
                                    {
                                        { 1, () => Console.WriteLine("Sunday. Relaxing...") },
                                        { 2, () => Console.WriteLine("Monday is the worst day of the week! 4 more days till it's week-end.") },
                                        { 3, () => Console.WriteLine("Tuesday. 3 days left till it's week-end.") },
                                        { 4, () => Console.WriteLine("Wednesday! Half way there...") },
                                        { 5, () => Console.WriteLine("Thursday. One day left!") },
                                        { 6, () => Console.WriteLine("Friday!! Party on!") },
                                        { 7, () => Console.WriteLine("Saturday... Hangover, but ready to party some more!") }
                                    }; 
        private static readonly Dictionary<string, Action<int>> Strategies = new Dictionary<string, Action<int>>
                                    {
                                        { "NO", day => DayInfoNo[day]() },
                                        { "US", day => DayInfoUs[day]() }           
                                    };

        static void Main(string[] args)
        {
            var country = args[0].ToUpper();

            if (!ValidCountries.Contains(country))
            {
                Console.WriteLine("Unrecognized country! Only " + String.Join(" and ", ValidCountries) + " are allowed!");
                return;
            }

            int dayOfWeek;
            bool inputOk;
            do
            {
                Console.WriteLine("Please enter day of week");
                inputOk = Int32.TryParse(Console.ReadLine(), out dayOfWeek);
            } while (!inputOk);
            ShowInfo(country, dayOfWeek);
        }

        public static void ShowInfo(string country, int dayOfWeek)
        {
            if (dayOfWeek < 1 || dayOfWeek > 7)
            {
                Console.WriteLine("This is not a valid day!");
                return;
            }
            Strategies[country](dayOfWeek);
        }
    }
}
