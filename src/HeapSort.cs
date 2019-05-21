using System;
using System.Collections.Generic;
using System.Text;

namespace SortingAlgorithms
{
    class HeapSort
    {
        static void MaxHeapify(int[] values, int index, int size)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            int largest;
            if (left <= size && values[left] > values[index])
                largest = left;
            else largest = index;
            if (right <= size && values[right] > values[largest])
                largest = right;

            if (largest != index)
            {
                int temp = values[index];
                values[index] = values[largest];
                values[largest] = temp;
                MaxHeapify(values, largest, size);
            }
        }

        static void BuildMaxHeap(int[] values)
        {
            for (int i = (int)((values.Length - 1) / 2); i >= 0; i--)
                MaxHeapify(values, i, values.Length - 1);
        }

        public static void Sort(int[] values)
        {
            int length = values.Length - 1;
            int temp;
            BuildMaxHeap(values);
            for (int i = length; i >= 1; i--)
            {
                temp = values[i];
                values[i] = values[0];
                values[0] = temp;
                length--;
                MaxHeapify(values, 0, length);
            }
        }
    }

    class Heap
    {
        public int Count;
        public int Depth;
        public int[] Content;

        public Heap(int[] input)
        {
            Count = input.Length;
            Content = input;
            GetDepth(Count);
            BuildMinHeap(Depth);
            CreateSortedArray();
        }

        void GetDepth(int insertCount)
        {
            Depth = Convert.ToString(insertCount, 2).Length;
        }

        void BuildMinHeap(int depth)
        {
            int parentBase = (int)Math.Pow(2, depth - 2) - 1;
            int childBase = (int)Math.Pow(2, depth - 1) - 1;//2^(depth-1)-1 to get to leftmost bottommost element
            int childMax = (int)Math.Pow(2, depth) - 1;

            //for (int parentLoop = parentBase; parentLoop < childBase; parentLoop++)
            for (int childIndex = childBase; childIndex < Count && childIndex < childMax; childIndex += 2)
            {
                //smallest value of the 3 has to be parent
                if (childIndex + 1 == Count)
                {
                    if (Content[childIndex] < Content[parentBase])
                    {
                        Swap(childIndex, parentBase);
                        CompareParentToChild(childIndex, (childIndex + 1) * 2 - 1);
                    }
                }
                else
                    CheckForSmallestChildParent(childIndex, parentBase);
                parentBase++;
            }
            if (depth > 2)
            {
                BuildMinHeap(depth - 1);
                return;
            }
            if (depth == 2)
                CheckForSmallestChildParent(1, 0);
        }

        void Swap(int a, int b)
        {
            int temp = Content[a];
            Content[a] = Content[b];
            Content[b] = temp;
        }

        void CheckForSmallestChildParent(int childIndex, int parentIndex)
        {
            if (Content[childIndex] < Content[parentIndex])
            {
                if (Content[childIndex + 1] < Content[childIndex])
                {
                    Swap(childIndex + 1, parentIndex);
                    CompareParentToChild(childIndex + 1, childIndex * 2 + 3);
                }
                else
                {
                    Swap(childIndex, parentIndex);
                    CompareParentToChild(childIndex, childIndex * 2 + 1);
                }
            }
            else
            {
                if (Content[childIndex + 1] < Content[parentIndex])
                {
                    Swap(childIndex + 1, parentIndex);
                    CompareParentToChild(childIndex + 1, childIndex * 2 + 3);
                }
            }
        }

        void Extract()
        {
            Swap(0, Count - 1);
            Count--;
            CompareParentToChild(0, 1);
        }

        void CompareParentToChild(int parentIndex, int childIndex)
        {
            if (parentIndex >= Count || childIndex >= Count)
                return;
            if (childIndex == Count - 1)
            {
                if (Content[childIndex] < Content[parentIndex])
                    Swap(childIndex, parentIndex);
                return;
            }
            if (Content[childIndex] < Content[childIndex + 1])
            {
                if (Content[childIndex] < Content[parentIndex])
                {
                    Swap(childIndex, parentIndex);
                    CompareParentToChild(childIndex, (childIndex + 1) * 2 - 1);
                }
            }
            else
            {
                if (Content[childIndex + 1] < Content[parentIndex])
                {
                    Swap(childIndex + 1, parentIndex);
                    CompareParentToChild(childIndex + 1, (childIndex + 2) * 2 - 1);
                }
            }
        }

        public void CreateSortedArray()
        {
            while (Count > 0)
                Extract();
        }
    }
}
