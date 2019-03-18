using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WHDataHelper
{
    internal class Replacer : SubProgram
    {
        public override void Execute(string[] args)
        {
            if (args.Length < 3)
            {
                PrintHelp();
                return;
            }

            try
            {
                string output = Replace(args[1], args[2]);
                Console.WriteLine(output);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                PrintHelp();
            }
        }

        public override void PrintHelp()
        {
            Console.Error.WriteLine("Zamiana: > WHDataHelper -r \"path/to/sourceFile\" \"path/to/templateFile\"");
        }


        private string Replace(string sourceUrl, string templateUrl)
        {
            string source = File.ReadAllText(sourceUrl);
            string[] templates = File.ReadAllLines(templateUrl);
            Dictionary<string, string> newValues = GetValuesDictionary(templates);

            foreach (string oldValue in newValues.Keys.ToArray().OrderByDescending(s => s.Length))
            {
                source = source.Replace(oldValue, newValues[oldValue]);
            }

            return source;
        }

        private static Dictionary<string, string> GetValuesDictionary(string[] templates)
        {
            Dictionary<string, string> newValues = new Dictionary<string, string>();

            foreach (string template in templates)
            {
                string[] pair = template.Split('\t');
                string id = pair[0];
                string name = pair[1];

                newValues[name] = id;
            }

            return newValues;
        }
    }
}