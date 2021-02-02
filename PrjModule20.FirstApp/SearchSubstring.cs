using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SubstringSearchLib;

namespace PrjModule20.FirstApp
{
    public static class SearchSubstring
    {
        /// <summary>
        ///     Multi-thread search of substring in files
        /// </summary>
        /// <param name="args">Array of files</param>
        /// <param name="substring">Substring to find</param>
        public static Task[] FindSubstrings(IEnumerable<string> args, string substring)
        {
            var tasks = new List<Task>();
            foreach (var file in args)
            {
                if (!File.Exists(file))
                    throw new ArgumentException($"File {file} doesn't exist");

                var fileFinderTread = new Task(() =>
                {
                    var start = DateTime.Now;
                    var searchResult = CheckStringExistence(file, substring);
                    if (searchResult != null && searchResult.EntryIndex == -1)
                    {
                        var consoleWriteThread = new Thread(DisplayResult);
                        consoleWriteThread.Start(searchResult);
                        while (consoleWriteThread.ThreadState != ThreadState.Stopped)
                        {
                        }
                    }

                    var end = DateTime.Now;
                    var ts = end - start;
                    Console.WriteLine($"\nApp 1 time taken: {ts}");
                });
                tasks.Add(fileFinderTread);
                fileFinderTread.Start();
            }

            return tasks.ToArray();
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

            return found
                ? new SearchResult {EntryIndex = pos - shift, FileName = file}
                : new SearchResult {EntryIndex = -1, FileName = file};
        }

        private static void DisplayResult(object searchResults)
        {
            switch (searchResults)
            {
                case null:
                    throw new ArgumentNullException(nameof(searchResults));
                case SearchResult resultsToDisplay:
                    Console.WriteLine(
                        $"Substring was found in file: {resultsToDisplay.FileName} on position: {resultsToDisplay.EntryIndex}");
                    break;
                default:
                    throw new ArgumentException(nameof(searchResults));
            }
        }
    }
}