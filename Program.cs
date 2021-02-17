// <copyright file="Program.cs" company="Rendall Koski">
// Copyright (c) Rendall Koski. All rights reserved.
// </copyright>

namespace WiktionaryScraper2
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using WiktionaryUtil;

    /// <summary>
    ///  Command-line entre to the WiktionaryUtil library
    /// </summary>
    public class Program
    {
        /// <summary>
        ///  Entry point to the program
        /// </summary>
        /// <param name="args">command-line arguments can be a term, a space-separated list of terms, or a path to a file that contains a list of terms</param>
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            if (args.Length == 0)
            {
                Hold("Supply a file path as command line argument. Press any key to close.");
                return;
            }

            string filePath = args.First();

            var words = File.Exists(filePath) ? File.ReadAllText(filePath, Encoding.UTF8).Split('\n').Select(w => w.Trim()).ToArray() : args;

            foreach (string word in words)
            {
                TestWord(word);

                if (Console.KeyAvailable)
                {
                     Hold("\nPress any key to continue.");
                }
            }

           Hold("\nPress any key to close.");
        }

        private static void TestWord(string term)
        {
            var termObj = Wiktionary.GetTerm(term);

            var termJson = JsonConvert.SerializeObject(termObj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            Console.Write(termJson);
        }

        private static void Hold(string p)
        {
            Output(p);
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            Console.ReadKey();
        }

        private static void Output(string p)
        {
            Console.Out.WriteLine(p);
        }
    }
}
