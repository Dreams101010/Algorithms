using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Interfaces;

namespace Algorithms.DataStructures
{
    public class ArrayStack<T> : IStack<T>
    {
        private DynamicArray<T> stack;
        private int topIndex;
        private const int EMPTY_INDEX = -1;

        public ArrayStack()
        {
            stack = new DynamicArray<T>();
            topIndex = EMPTY_INDEX;
        }

        public void Push(T item)
        {
            stack.Add(item);
            topIndex++;
        }

        public T Pop()
        {
            if (topIndex == EMPTY_INDEX)
            {
                throw new InvalidOperationException("Stack was empty.");
            }
            var val = stack.Remove(topIndex);
            topIndex--;
            return val;
        }

        public T Peek()
        {
            if (topIndex == EMPTY_INDEX)
            {
                throw new InvalidOperationException("Stack was empty.");
            }
            var val = stack[topIndex];
            return val;
        }

        public bool IsEmpty()
        {
            return topIndex == EMPTY_INDEX;
        }
    }
}
