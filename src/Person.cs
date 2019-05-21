using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Comment { get; set; }

        public static Person[] OrderByExample(Person[] persons)
        {
            return persons.OrderBy(person => person.Name).ToArray();
        }

        public static Person[] InsertionSort(Person[] persons, Func<Person, Person, int> comparer)
        {
            return InsertionSort(persons, 0, persons.Length, comparer);
        }

        static Person[] InsertionSort(Person[] persons, int start, int end, Func<Person, Person, int> comparer)
        {
            Person[] result = new Person[persons.Length];
            persons.CopyTo(result, 0);
            Person temp;

            for (int i = start + 1; i < end; i++)
                for (int k = i; k > 0; k--)
                {
                    //if (result[k].Name.CompareTo(result[k - 1].Name) > 0)
                    if (comparer(result[k], result[k - 1]) < 0)
                    {
                        temp = result[k - 1];
                        result[k - 1] = result[k];
                        result[k] = temp;
                    }
                    else break;
                }
            return result;
        }

        public static Person[] QuickSort(Person[] persons, Func<Person, Person, int> comparer)
        {
            Person[] result = new Person[persons.Length];
            persons.CopyTo(result, 0);
            QuickSort(result, 0, persons.Length - 1, comparer);
            return result;
        }

        static void QuickSort(Person[] persons, int start, int end, Func<Person, Person, int> comparer)
        {
            if (start >= end)
                return;

            int pivotIndex = (start + end) / 2;
            Person pivotValue = persons[pivotIndex];
            Person temp = persons[pivotIndex];
            persons[pivotIndex] = persons[end];
            persons[end] = temp;
            int storeIndex = start;
            for (int i = start; i < end; i++)
            {
                //comparer(result[k], result[k - 1]) < 0
                //if (values[i] < pivotValue)
                if (comparer(persons[i], pivotValue) < 0)
                {
                    temp = persons[i];
                    persons[i] = persons[storeIndex];
                    persons[storeIndex] = temp;
                    storeIndex += 1;
                }
            }
            temp = persons[storeIndex];
            persons[storeIndex] = persons[end];
            persons[end] = temp;

            QuickSort(persons, start, storeIndex - 1, comparer);
            QuickSort(persons, storeIndex + 1, end, comparer);
        }

        static void MaxHeapify(Person[] values, int index, int size, Func<Person, Person, int> comparer)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            int largest;
            //if (left <= size && values[left] > values[index])
            if (left <= size && comparer(values[left], values[index]) > 0)
                largest = left;
            else largest = index;
            //if (right <= size && values[right] > values[largest])
            if (right <= size && comparer(values[right], values[largest]) > 0)
                largest = right;

            if (largest != index)
            {
                Person temp = values[index];
                values[index] = values[largest];
                values[largest] = temp;
                MaxHeapify(values, largest, size, comparer);
            }
        }

        static void BuildMaxHeap(Person[] values, Func<Person, Person, int> comparer)
        {
            for (int i = (int)((values.Length - 1) / 2); i >= 0; i--)
                MaxHeapify(values, i, values.Length - 1, comparer);
        }

        public static Person[] Heapsort(Person[] values, Func<Person, Person, int> comparer)
        {
            Person[] result = new Person[values.Length];
            values.CopyTo(result, 0);

            int length = result.Length - 1;
            Person temp;

            BuildMaxHeap(result, comparer);

            for (int i = length; i >= 1; i--)
            {
                temp = result[i];
                result[i] = result[0];
                result[0] = temp;
                length--;
                MaxHeapify(result, 0, length, comparer);
            }

            return result;
        }
    }
}
