using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortingAlgorithms
{
    class BinaryTree
    {
        private int? Value;
        private BinaryTree Left;
        private BinaryTree Right;

        public void Insert(int value)
        {
            if (!Value.HasValue)
            {
                Value = value;
                Left = new BinaryTree();
                Right = new BinaryTree();
            }
            else (Value.Value > value ? Left : Right).Insert(value);
        }

        public IEnumerable<int> GetSorted()
        {
            if (!Value.HasValue)
                yield break;

            foreach (var value in Left.GetSorted())
                yield return value;
            yield return Value.Value;
            foreach (var value in Right.GetSorted())
                yield return value;
        }

        public static int[] Sort(int[] unsorted)
        {
            var tree = new BinaryTree();
            foreach (var value in unsorted)
                tree.Insert(value);
            return tree.GetSorted().ToArray();
        }
    }
}
