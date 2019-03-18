using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHDataHelper
{
    public class Program
    {
        private static readonly Dictionary<string, SubProgram> programs = new Dictionary<string, SubProgram>()
        {
            { "-r", new Replacer() }
        };

        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                PrintHelp();

                return;
            }


            if (programs.TryGetValue(args[0], out SubProgram program))
            {
                program.Execute(args);
            }
            else
            {
                PrintHelp();
            }
        }

        private static void PrintHelp()
        {
            Console.Error.WriteLine("Podaj wszystkie argumenty.");
            Console.Error.WriteLine("Możliwe opcje:");

            foreach (SubProgram program in programs.Values)
            {
                program.PrintHelp();
            }
        }
    }
}
