using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.Interfaces;

namespace Algorithms.DataStructures
{
    public class ArrayStack<T> : IStack<T>, IEnumerable<T>, IReadOnlyCollection<T>
    {
        private DynamicArray<T> stack;
        private int topIndex;
        private int version = 0;
        private const int EMPTY_INDEX = -1;

        public int Count => topIndex + 1;

        public ArrayStack()
        {
            stack = new DynamicArray<T>();
            topIndex = EMPTY_INDEX;
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return stack[index];
            }
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

        /// <summary>
        /// Checks if index is in bounds of the stack.
        /// </summary>
        /// <param name="index">Index of element in the dynamic array.</param>
        /// <returns>true, if index is in bounds, false otherwise</returns>
        private bool IsIndexValid(int index)
        {
            bool isInBounds = index >= 0 && index <= topIndex;
            if (isInBounds)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Throws exception if given index is out of bounds.
        /// </summary>
        /// <param name="index">Index to validate.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        private void ValidateIndex(int index)
        {
            if (!IsIndexValid(index))
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>
        {
            private ArrayStack<T> stack;
            private int version;
            private int index;
            private T current;
            public T Current => current;
            object IEnumerator.Current => current;

            public Enumerator(ArrayStack<T> stack)
            {
                this.stack = stack;
                version = stack.version;
                index = 0;
                current = default(T);
            }

            public void Dispose()
            { }

            public bool MoveNext()
            {
                if (version != stack.version)
                {
                    throw new InvalidOperationException("Enumeration failed: collection changed");
                }
                if (index >= stack.topIndex) // last element
                {
                    current = default(T);
                    return false;
                }
                current = stack[index];
                index++;
                return true;
            }

            public void Reset()
            {
                version = stack.version;
                index = 0;
            }
        }
    }
}
