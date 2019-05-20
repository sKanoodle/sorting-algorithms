using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SortingAlgorithms
{
    class Program
    {
        static readonly Random random = new Random();

        static void Main(string[] args)
        {
            MeasureTreeSort(10);

            Console.ReadKey();
        }

        private static void MeasureTreeSort(int repetitions)
        {
            TimeSpan Measure(int[] array)
            {
                var watch = Stopwatch.StartNew();
                var sorted = BinaryTree.Sort(array);
                return watch.Elapsed;
            }

            var unsorted = Enumerable.Range(0, 1_000_000).Select(_ => random.Next()).ToArray();
            TimeSpan time = new TimeSpan();
            Measure(unsorted);

            for (int i = 0; i < repetitions; i++)
                time += Measure(unsorted);
            Console.WriteLine($"treesort: {time / repetitions}");
        }

        

    }

    
}
