using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using PrjModule20.FirstApp;
using PrjModule20.SecondApp;
namespace PrjModule20
{
    internal static class Program
    {
        private static void Main(string[] args)
        {


            #region 1 small file
            args = new[] { "File1.txt" };

            var task1Ended = FirstApp.SearchSubstring.FindSubstrings(args, "Justo donec");
            while (!task1Ended)
            {
            }

            Console.WriteLine($"App 1 finished\n");

            var task2Ended = SecondApp.SearchSubstring.SearchEvenWords(args);
            while (!task2Ended)
            {
            }
            Console.WriteLine($"App 2 finished\n");

            #endregion

            #region 2 small+medium file
            args = new[] { "File1.txt", "File2.txt" };

            task1Ended = FirstApp.SearchSubstring.FindSubstrings(args, "Justo donec");
            while (!task1Ended)
            {
            }

            Console.WriteLine($"App 1 finished\n");

            task2Ended = SecondApp.SearchSubstring.SearchEvenWords(args);
            while (!task2Ended)
            {
            }
            Console.WriteLine($"App 2 finished\n");

            #endregion

            #region 3 small+medium+big file
            args = new[] { "File1.txt", "File2.txt", "File3.txt" };

            task1Ended = FirstApp.SearchSubstring.FindSubstrings(args, "Justo donec");
            while (!task1Ended)
            {
            }

            Console.WriteLine($"App 1 finished\n");

            task2Ended = SecondApp.SearchSubstring.SearchEvenWords(args);
            while (!task2Ended)
            {
            }
            Console.WriteLine($"App 2 finished\n");

            #endregion

        }
    }
}
