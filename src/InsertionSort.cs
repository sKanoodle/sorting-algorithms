using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    class InsertionSort
    {
        public static void Sort_InPlace(int[] input)
        {
            Sort_InPlace(input, 0, input.Length);
        }

        public static void Sort_InPlace(int[] input, int start, int end)
        {
            int temp;
            for (int i = start + 1; i < end; i++)
                for (int k = i; k > start; k--)
                {
                    if (input[k] < input[k - 1])
                    {
                        temp = input[k - 1];
                        input[k - 1] = input[k];
                        input[k] = temp;
                    }
                    else break;
                }
        }
    }
}
