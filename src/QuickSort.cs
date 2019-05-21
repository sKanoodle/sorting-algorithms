using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    class QuickSort
    {
        public static void Sort(int[] values)
        {
            Sort(values, 0, values.Length - 1);
        }

        public static void Sort2(int[] values)
        {
            Sort2(values, 0, values.Length - 1);
        }

        public static void Sort3(int[] values)
        {
            Sort3(values, 0, values.Length - 1);
        }

        static void Sort(int[] values, int start, int end)
        {
            if (start >= end)
                return;

            int pivotIndex = (start + end) / 2;
            int pivotValue = values[pivotIndex];
            int temp = values[pivotIndex];
            values[pivotIndex] = values[end];
            values[end] = temp;
            int storeIndex = start;
            for (int i = start; i < end; i++)
            {
                if (values[i] < pivotValue)
                {
                    temp = values[i];
                    values[i] = values[storeIndex];
                    values[storeIndex] = temp;
                    storeIndex += 1;
                }
            }
            temp = values[storeIndex];
            values[storeIndex] = values[end];
            values[end] = temp;

            QuickSort.Sort(values, start, storeIndex - 1);
            QuickSort.Sort(values, storeIndex + 1, end);
        }

        static void Sort2(int[] values, int start, int end)
        {
            //if (start >= end)
            //    return;

            int pivotIndex = (start + end) / 2;
            int pivotValue = values[pivotIndex];
            int temp = values[pivotIndex];
            values[pivotIndex] = values[end];
            values[end] = temp;
            int storeIndex = start;
            for (int i = start; i < end; i++)
            {
                if (values[i] < pivotValue)
                {
                    temp = values[i];
                    values[i] = values[storeIndex];
                    values[storeIndex] = temp;
                    storeIndex += 1;
                }
            }
            temp = values[storeIndex];
            values[storeIndex] = values[end];
            values[end] = temp;

            if (start + 16 > storeIndex)
                InsertionSort.Sort_InPlace(values, start, storeIndex);
            else Sort2(values, start, storeIndex - 1);
            if (storeIndex + 16 > end)
                InsertionSort.Sort_InPlace(values, storeIndex + 1, end + 1);
            else Sort2(values, storeIndex + 1, end);
        }

        static void Sort3(int[] values, int start, int end)
        {
            int pivotIndex = (start + end) / 2;
            int pivotValue = values[pivotIndex];
            int temp = values[pivotIndex];
            values[pivotIndex] = values[end];
            values[end] = temp;
            int storeIndex = start;
            for (int i = start; i < end; i++)
            {
                if (values[i] < pivotValue)
                {
                    temp = values[i];
                    values[i] = values[storeIndex];
                    values[storeIndex] = temp;
                    storeIndex += 1;
                }
            }
            temp = values[storeIndex];
            values[storeIndex] = values[end];
            values[end] = temp;

            if (start + 7 > storeIndex)
                InsertionSort.Sort_InPlace(values, start, storeIndex);
            else Sort3(values, start, storeIndex - 1);
            if (storeIndex + 7 > end)
                InsertionSort.Sort_InPlace(values, storeIndex + 1, end + 1);
            else Sort3(values, storeIndex + 1, end);
        }

        public static List<int> Sort4(List<int> input)
        {
            List<int> list = input;
            List<int> result = new List<int>();
            int pivot = list[(int)(list.Count / 4)];

            List<int> smaller = new List<int>();
            List<int> larger = new List<int>();
            List<int> pivotList = new List<int>();
            foreach (int number in list)
                if (number < pivot)
                    smaller.Add(number);
                else if (number == pivot)
                    pivotList.Add(number);
                else
                    larger.Add(number);

            if (smaller.Count > 0)
                result.AddRange(Sort4(smaller));
            result.AddRange(pivotList);
            if (larger.Count > 0)
                result.AddRange(Sort4(larger));
            return result;
        }

        public static List<int> Sort5(List<int> input)
        {
            List<int> result = new List<int>();
            int pivot = input[(int)(input.Count / 4)];

            List<int> smaller = new List<int>();
            List<int> larger = new List<int>();
            List<int> pivotList = new List<int>();
            foreach (int number in input)
                if (number < pivot)
                    smaller.Add(number);
                else if (number == pivot)
                    pivotList.Add(number);
                else
                    larger.Add(number);

            if (smaller.Count > 20)
                result.AddRange(Sort5(smaller));
            else
                result.AddRange(BubbleSort.Sort(smaller));
            result.AddRange(pivotList);
            if (larger.Count > 20)
                result.AddRange(Sort5(larger));
            else
                result.AddRange(BubbleSort.Sort(larger));
            return result;
        }
    }
}
