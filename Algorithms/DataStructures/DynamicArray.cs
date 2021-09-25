using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.DataStructures
{
    public class DynamicArray<T>
    {
        private T[] array;
        private int size;
        private int capacity;
        // (2^31 - 2) / 2
        private const int MAX_DOUBLABLE_CAPACITY = 1_073_741_823;
        private const int MAX_CAPACITY = int.MaxValue;

        public DynamicArray() : this(1)
        {

        }

        public DynamicArray(int initialCapacity)
        {
            if (initialCapacity <= 0)
            {
                throw new ArgumentException("Cannot create resizing array with zero or negative capacity");
            }
            array = new T[initialCapacity];
            capacity = initialCapacity;
            Size = 0;
        }

        public int Size
        {
            get
            {
                return size;
            }
            private set
            {
                size = value;
            }
        }

        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return array[index];
            }
            set
            {
                ValidateIndex(index);
                array[index] = value;
            }
        }

        public void Add(T item)
        {
            if (capacity == Size)
            {
                Grow();
            }
            array[Size] = item;
            Size++;
        }

        public void InsertAt(int index, T value)
        {
            ValidateIndex(index);
            if (Size + 1 > capacity)
            {
                Grow();
            }
            ShiftRightAtIndex(index);
            array[index] = value;
        }

        public T Remove(int index)
        {
            ValidateIndex(index);
            T returnValue = array[index];
            ShiftLeftAtIndex(index);
            return returnValue;
        }

        public void Clear()
        {
            Size = 0;
        }

        public void EnsureCapacity(int newCapacity)
        {
            while (capacity < newCapacity)
            {
                Grow();
            }
        }

        private void Grow()
        {
            int newCapacity;
            if (capacity <= MAX_DOUBLABLE_CAPACITY)
            {
                newCapacity = capacity * 2;
            }
            else
            {
                newCapacity = MAX_CAPACITY;
            }
            T[] newArray = new T[newCapacity];
            Array.Copy(array, newArray, Size);
            array = newArray;
            capacity = newCapacity;
        }

        private bool IsIndexValid(int index)
        {
            if (index >= 0 && index < Size)
            {
                return true;
            }
            return false;
        }

        private void ValidateIndex(int index)
        {
            if (!IsIndexValid(index))
            {
                throw new IndexOutOfRangeException("Index is out of range");
            }
        }

        private void ShiftLeftAtIndex(int index)
        {
            for (int i = index; i < Size - 1; i++)
            {
                array[i] = array[i + 1];
            }
            Size--;
        }

        private void ShiftRightAtIndex(int index)
        {
            for (int i = Size - 1; i >= index; i--)
            {
                array[i + 1] = array[i];
            }
            Size++;
        }
    }
}
