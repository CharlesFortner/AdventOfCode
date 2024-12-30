using AdventOfCode.Utils;

namespace AdventOfCode
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Advent Of Code");
            Console.Write("Choose a year: ");
            var yearStr = Console.ReadLine();
            if (yearStr.Equals("Test", StringComparison.InvariantCultureIgnoreCase))
            {
                Test();
                goto End;
            }

            while (!int.TryParse(yearStr, out int year) || year < 2015 || year > 2024)
            {
                Console.Write("Invalid year\nChoose a year: ");
                yearStr = Console.ReadLine();
                if (yearStr.Equals("Test", StringComparison.InvariantCultureIgnoreCase))
                {
                    Test();
                    goto End;
                }
            }

            Console.WriteLine($"Chosen year: {yearStr}");
            Console.Write("Choose a day: ");
            var dayStr = Console.ReadLine();

            while (!int.TryParse (dayStr, out int day) || day < 1 || day > 25)
            {
                Console.Write("Invalid day\nChoose a day: ");
                dayStr = Console.ReadLine();
            }

            var aocTypeName = $"AdventOfCode.AoC{yearStr}.Day{dayStr.PadLeft(2,'0')}";

            var assembly = typeof(Program).Assembly;
            var type = assembly.GetType(aocTypeName);

            if (type == null)
            {
                Console.WriteLine("This day has not been completed!");
                return;
            }

            if (Activator.CreateInstance(type) is not AoCDay aocDay)
            {
                Console.WriteLine("This day is not of the correct type!");
                return;
            }

            Console.Write("Which part would you like to run (1, 2, both)? ");
            var part = Console.ReadLine();

            while (part != "1" && part != "2" && !part.Equals("both", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Invalid part\nWhich part would you like to run (1, 2, both)? ");
                part = Console.ReadLine();
            }

            if (part == "1" || part.Equals("both", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    Console.WriteLine($"Part 1: {aocDay.Part1()}");
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("This part hasn't been completed!");
                }
            }

            if (part == "2" || part.Equals("both", StringComparison.InvariantCultureIgnoreCase))
            {
                try
                {
                    Console.WriteLine($"Part 2: {aocDay.Part2()}");
                }
                catch (NotImplementedException)
                {
                    Console.WriteLine("This part hasn't been completed!");
                }
            }

        End:
            Console.WriteLine("");
        }

        public static void Test()
        {
            Console.WriteLine("This is a test");
        }
    }
}
