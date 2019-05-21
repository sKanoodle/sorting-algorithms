using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    class BubbleSort
    {
        public static int[] Sort(int[] input)
        {
            int[] array = input;
            int temp;
            int length = input.Length;
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
            return array;
        }

        public static List<int> Sort(List<int> input)
        {
            int temp;
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = 0; i < input.Count - 1; i++)
                {
                    if (input[i] > input[i + 1])
                    {
                        temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
            return input;
        }

        public static LinkedList<int> Sort(LinkedList<int> input)
        {
            LinkedList<int> list = input;
            LinkedListNode<int> node;
            int temp;
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                node = list.First;
                while (!node.Equals(node.List.Last))
                {
                    if (node.Value > node.Next.Value)
                    {
                        temp = node.Value;
                        node.Value = node.Next.Value;
                        node.Next.Value = temp;
                        sorted = false;
                    }
                    node = node.Next;
                }
            }
            return list;
        }

        public static void Sort_InPlace(int[] input)
        {
            Sort_InPlace(input, 0, input.Length - 1);
        }

        static void Sort_InPlace(int[] input, int start, int end)
        {
            int temp;
            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                for (int i = start; i < end; i++)
                {
                    if (input[i] > input[i + 1])
                    {
                        temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        sorted = false;
                    }
                }
            }
        }

        public static void Sort_InPlace2(int[] input)
        {
            Sort_InPlace2(input, 0, input.Length - 1);
        }

        static void Sort_InPlace2(int[] input, int start, int end)
        {
            int temp;
            int newend;
            while (end > 0)
            {
                newend = 0;
                for (int i = start; i < end; i++)
                {
                    if (input[i] > input[i + 1])
                    {
                        temp = input[i];
                        input[i] = input[i + 1];
                        input[i + 1] = temp;
                        newend = i;
                    }
                }
                end = newend;
            }
        }
    }
}
