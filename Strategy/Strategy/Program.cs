using System;
using System.Linq;

namespace Strategy
{
    class Program
    {
        private static readonly string[] ValidCountries = new[] { "NO", "US" };

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
            if (country == "NO")
            {
                switch (dayOfWeek)
                {
                    case 1:
                        Console.WriteLine("Mandag er den verste dagen i uken! Fire dager igjen til det er helg.");
                        break;
                    case 2:
                        Console.WriteLine("Tirsdag. Tre dager igjen til helg.");
                        break;
                    case 3:
                        Console.WriteLine("Onsdag... Lillelørdag!");
                        break;
                    case 4:
                        Console.WriteLine("Torsdag. Nærmer oss helgen nå!");
                        break;
                    case 5:
                        Console.WriteLine("Fredag!! Party on!");
                        break;
                    case 6:
                        Console.WriteLine("Lørdag. Klar for mer fest!");
                        break;
                    case 7:
                        Console.WriteLine("Søndag. Sliten nå...");
                        break;
                    default:
                        Console.WriteLine("This is not a valid day...");
                        break;
                }
            }
            else if (country == "US")
            {
                switch (dayOfWeek)
                {
                    case 1:
                        Console.WriteLine("Sunday. Relaxing...");
                        break;
                    case 2:
                        Console.WriteLine("Monday is the worst day of the week! 4 more days till it's week-end.");
                        break;
                    case 3:
                        Console.WriteLine("Tuesday. 3 days left till it's week-end.");
                        break;
                    case 4:
                        Console.WriteLine("Wednesday! Half way there...");
                        break;
                    case 5:
                        Console.WriteLine("Thursday. One day left!");
                        break;
                    case 6:
                        Console.WriteLine("Friday!! Party on!");
                        break;
                    case 7:
                        Console.WriteLine("Saturday... Hangover, but ready to party some more!");
                        break;
                    default:
                        Console.WriteLine("This is not a valid day...");
                        break;
                }
            }
        }
    }
}
