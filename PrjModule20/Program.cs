using System;
using System.Threading.Tasks;
using PrjModule20.FirstApp;

namespace PrjModule20
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            #region 1 small file

            args = new[] {"File1.txt"};

            var task1 = SearchSubstring.FindSubstrings(args, "Justo donec");
            Task.WaitAll(task1);

            Console.WriteLine("App 1 finished\n");

            var task2 = SecondApp.SearchSubstring.SearchEvenWords(args);
            Task.WaitAll(task2);

            Console.WriteLine("App 2 finished\n");

            #endregion

            #region 2 small+medium file

            args = new[] {"File1.txt", "File2.txt"};

            task1 = SearchSubstring.FindSubstrings(args, "Justo donec");
            Task.WaitAll(task1);

            Console.WriteLine("App 1 finished\n");

            task2 = SecondApp.SearchSubstring.SearchEvenWords(args);
            Task.WaitAll(task2);

            Console.WriteLine("App 2 finished\n");

            #endregion

            #region 3 small+medium+big file

            args = new[] {"File1.txt", "File2.txt", "File3.txt"};

            task1 = SearchSubstring.FindSubstrings(args, "Justo donec");
            Task.WaitAll(task1);

            Console.WriteLine("App 1 finished\n");

            task2 = SecondApp.SearchSubstring.SearchEvenWords(args);
            Task.WaitAll(task2);

            Console.WriteLine("App 2 finished\n");

            #endregion
        }
    }
}