using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PrjModule20.SecondApp
{
    public class SearchSubstring
    {

        /// <summary>
        ///     Multi-thread display words on even position
        /// </summary>
        public static Task[] SearchEvenWords(string[] args)
        {
            var tasks = new List<Task>();
            foreach (var file in args)
            {
                if (!File.Exists(file))
                    throw new ArgumentException($"File {file} doesn't exist");

                var evenWordFinder = new Task((() => DisplayEvenWords(file)));
                tasks.Add(evenWordFinder);
                evenWordFinder.Start();
            }

            Thread.Sleep(1);
            return tasks.ToArray();
        }

        private static void DisplayEvenWords(object file)
        {
            switch (file)
            {
                case null:
                    throw new ArgumentNullException(nameof(file));
                case string resultsToDisplay:
                    var start = DateTime.Now;
                    string line;
                    var readFile = new StreamReader(resultsToDisplay);
                    while ((line = readFile.ReadLine()) != null)
                    {
                        var words = line.Replace("  ", " ").Split(' ');
                        for (var i = words.Length - 1; i >= 0; i--)
                            if (i % 2 == 0)
                                Console.Write(words[i] + " ");
                    }

                    var end = DateTime.Now;
                    var ts = end - start;
                    Console.WriteLine($"\nApp 2 time taken: {ts}");

                    break;
                default:
                    throw new ArgumentException(nameof(file));
            }
        }
    }
}