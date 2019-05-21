using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SortingAlgorithms
{
    //4 sortieralgorhythmen
    //2 stabile
    //2 instabile

    //personen sortieren (vorname, nachname, alter, anmerkung)

    //list sort
    //array sort
    class Program
    {
        static bool run = true;
        const int Iterations = 100;
        static readonly int length = 5000;
        static Random random = new Random();

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

        static void Test<T>(Func<List<T>, List<T>> algorithm, T[] testValues)
        {
            List<T> initialValues = null;
            Action init = () => initialValues = new List<T>(testValues);
            Action run = () => algorithm(initialValues);
            Test(GetName(algorithm), init, run);
        }

        static void Test<T>(Action<T[]> algorithm, T[] testValues)
        {
            T[] initialValues = null;
            Action init = () =>
            {
                initialValues = new T[testValues.Length];
                Array.Copy(testValues, initialValues, testValues.Length);
            };
            Action run = () => algorithm(initialValues);
            Test(GetName(algorithm), init, run);
        }

        static void Test<T>(List<T> testValues)
        {
            List<T> initialValues = null;
            Action init = () => initialValues = new List<T>(testValues);
            Action run = () => initialValues.Sort();
            Test("list.Sort", init, run);
        }

        static void TestHeap(int[] testValues)
        {
            int[] initialValues = null;
            Action init = () =>
            {
                initialValues = new int[testValues.Length];
                Array.Copy(testValues, initialValues, testValues.Length);
            };
            Action run = () => new Heap(initialValues);
            Test("HeapSort", init, run);
        }

        static void Test(string name, Action init, Action run)
        {
            init();
            run();
            long[] runtimes = new long[Iterations];
            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < Iterations; i++)
            {
                init();
                watch.Restart();
                run();
                runtimes[i] = watch.ElapsedTicks;
            }
            Array.Sort(runtimes);
            runtimes = RemoveOutliners(runtimes);
            long min = runtimes[0];
            long max = runtimes[runtimes.Length - 1];
            double average = runtimes.Average();
            double median = Median(runtimes);
            Console.WriteLine("{0,-24}|{1,8:0.}average {2,8}median {3,8}min {4,8}max", name, average, median, min, max);
        }

        static T[] RemoveOutliners<T>(T[] sortedValues)
        {
            int cutoff = (int)(sortedValues.Length * 0.1);
            T[] result = new T[sortedValues.Length - 2 * cutoff];
            Array.Copy(sortedValues, cutoff, result, 0, result.Length);
            return result;
        }

        static double Median(long[] sortedValues)
        {
            int mid = sortedValues.Length / 2;
            bool isUnevenLength = sortedValues.Length % 2 == 1;
            if (isUnevenLength)
                return sortedValues[mid];
            else
                return (sortedValues[mid] + sortedValues[mid + 1]) / 2;
        }

        static string GetName(Delegate d)
        {
            System.Reflection.MethodInfo method = d.Method;
            string result = string.Empty;

            if (method.DeclaringType != typeof(Program))
                result = method.DeclaringType.Name + ".";

            result += method.Name;
            return result;
        }

        static void Main(string[] args)
        {
            Person[] persons = new Person[2000];
            for (int i = 0; i < persons.Length; i++)
            {
                persons[i] = new Person();
                persons[i].Name = GetRandomString();
                persons[i].Surname = GetRandomString();
                persons[i].Age = random.Next(101);
                persons[i].Comment = GetRandomString();
            }

            Person[] personsSortedTemp = Person.InsertionSort(persons, (Person x, Person y) => x.Surname.CompareTo(y.Surname));
            Person[] personsSorted = Person.InsertionSort(personsSortedTemp, (Person x, Person y) => x.Name.CompareTo(y.Name));

            Person[] personsQuickSortedTemp = Person.QuickSort(persons, (Person x, Person y) => x.Surname.CompareTo(y.Surname));
            Person[] personsQuickSorted = Person.QuickSort(personsQuickSortedTemp, (Person x, Person y) => x.Name.CompareTo(y.Name));

            Person[] personsHeapSortedTemp = Person.Heapsort(persons, (Person x, Person y) => x.Surname.CompareTo(y.Surname));
            Person[] personsHeapSorted = Person.Heapsort(personsHeapSortedTemp, (Person x, Person y) => x.Name.CompareTo(y.Name));

            //foreach (Person person in personsSortedTemp)
            //{
            //    Console.WriteLine("{0, -15} {1, -15}, {2, 3} ({3, -15})", person.Surname, person.Name, person.Age, person.Comment);
            //}
            //Console.WriteLine("-------------------------------------------------------------------------------");
            //foreach (Person person in personsSorted)
            //{
            //    Console.WriteLine("{0, -15} {1, -15}, {2, 3} ({3, -15})", person.Surname, person.Name, person.Age, person.Comment);
            //}
            //Console.WriteLine("-------------------------------------------------------------------------------");
            //foreach (Person person in personsQuickSortedTemp)
            //{
            //    Console.WriteLine("{0, -15} {1, -15}, {2, 3} ({3, -15})", person.Surname, person.Name, person.Age, person.Comment);
            //}
            //Console.WriteLine("-------------------------------------------------------------------------------");
            foreach (Person person in personsHeapSorted)
            {
                Console.WriteLine("{0, -15} {1, -15}, {2, 3} ({3, -15})", person.Surname, person.Name, person.Age, person.Comment);
            }
            Console.ReadKey();
        }

        static string GetRandomString()
        {
            int runs = random.Next(5, 8);
            string result = string.Empty;
            for (int i = 0; i < runs; i++)
                result += (char)random.Next(97, 123); //97, 123 to get all lowercase letters
            return result;
        }

        static void Tests()
        {
            while (run)
            {
                int[] array = new int[length];
                for (int i = 0; i < length; i++)
                    array[i] = random.Next(10000000);
                //for (int i = 0; i < length; i++)
                //    array[i] = length - i;
                List<int> list = new List<int>(array);

                Test(QuickSort.Sort, array);
                Test(QuickSort.Sort2, array);
                Test(QuickSort.Sort3, array);
                Test(QuickSort.Sort4, array);
                Test(QuickSort.Sort5, array);
                Test(BubbleSort.Sort_InPlace, array);
                Test(BubbleSort.Sort_InPlace2, array);
                Test(InsertionSort.Sort_InPlace, array);
                TestHeap(array);
                Test(HeapSort.Sort, array);
                Test(Array.Sort, array);
                Test(list);//list.Sort

                for (int i = 0; i < length; i++)
                    array[i] = random.Next(10000000);
                HeapSort.Sort(array);
                for (int i = 0; i < array.Length - 1; i++)
                    if (array[i] > array[i + 1])
                        Console.WriteLine("error: {0} not smaller than {1}", array[i], array[i + 1]);



                if (Console.ReadKey().Key != ConsoleKey.Spacebar)
                    run = false;
                Console.Clear();
            }
        }

    }
}
