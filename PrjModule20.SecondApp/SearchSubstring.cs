using System;
using System.IO;
using System.Threading;

namespace PrjModule20.SecondApp
{
    public class SearchSubstring
    {
        private static readonly SemaphoreSlim Sem;

        static SearchSubstring()
        {
            Sem = new SemaphoreSlim(10);
        }

        /// <summary>
        ///     Multi-thread display words on even position
        /// </summary>
        public static bool SearchEvenWords(string[] args)
        {
            foreach (var file in args)
            {
                if (!File.Exists(file))
                    throw new ArgumentException($"File {file} doesn't exist");

                var evenWordFinder = new Thread(DisplayEvenWords);
                evenWordFinder.Start(file);
            }

            Thread.Sleep(1);
            while (Sem.CurrentCount != 10)
            {
            }

            return true;
        }

        private static void DisplayEvenWords(object file)
        {
            switch (file)
            {
                case null:
                    throw new ArgumentNullException(nameof(file));
                case string resultsToDisplay:
                    Sem.Wait();
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
                    Console.WriteLine($"App 2 ThreadID: {Thread.CurrentThread.ManagedThreadId} time taken: {ts}");

                    Sem.Release();
                    break;
                default:
                    throw new ArgumentException(nameof(file));
            }
        }
    }
}