using SubstringSearchLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace PrjModule20.FirstApp
{
    public static class SearchSubstring
    {
        public static bool FindSubstrings(IEnumerable<string> args, string substring)
        {
            var threads = new List<Thread>();
            foreach (var file in args)
            {
                if (!File.Exists(file))
                    throw new ArgumentException($"File {file} doesn't exist");

                var fileFinderTread = new Thread((() =>
                {
                    var start = DateTime.Now;
                    var searchResult = CheckStringExistence(file, substring);
                    if (searchResult != null && searchResult.EntryIndex == -1)
                    {
                        var consoleWriteThread = new Thread(DisplayResult);
                        consoleWriteThread.Start(searchResult);
                        while (consoleWriteThread.ThreadState != System.Threading.ThreadState.Stopped)
                        {
                        }
                    }

                    var end = DateTime.Now;
                    var ts = (end - start);
                    Console.WriteLine($"App 1 ThreadID: {Thread.CurrentThread.ManagedThreadId} time taken: {ts}");

                }));
                threads.Add(fileFinderTread);
                fileFinderTread.Start();
            }

            while (threads.FindIndex(t => t.ThreadState != System.Threading.ThreadState.Stopped) != -1)
            {
            }

            return true;
        }

        private static SearchResult CheckStringExistence(string file, string substring)
        {
            var shift = 0;
            var found = false;
            var pos = 0;
            using var myStreamReader = new StreamReader(file);

            while (!myStreamReader.EndOfStream)
            {
                var ch = myStreamReader.Read();
                if (ch == substring[shift])
                {
                    if (shift + 1 == substring.Length)
                    {
                        found = true;
                        break;
                    }

                    shift++;
                }
                else
                {
                    shift = 0;
                }

                pos++;
            }

            myStreamReader.Close();

            return found ? new SearchResult() { EntryIndex = pos - shift, FileName = file } : new SearchResult() { EntryIndex = -1, FileName = file };
        }

        private static void DisplayResult(object searchResults)
        {
            switch (searchResults)
            {
                case null:
                    throw new ArgumentNullException(nameof(searchResults));
                case SearchResult resultsToDisplay:
                    //Console.WriteLine($"Substring was found in file: {resultsToDisplay.FileName} on position: {resultsToDisplay.EntryIndex}");
                    break;
                default:
                    throw new ArgumentException(nameof(searchResults));
            }
        }

    }
}