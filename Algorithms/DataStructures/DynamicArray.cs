using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.DataStructures
{
    /// <summary>
    /// Dynamic array implementation.
    /// </summary>
    /// <typeparam name="T">Type of elements in dynamic array.</typeparam>
    public class DynamicArray<T>
    {
        // Internal array
        private T[] array;
        // Current size (how many elements array actually contains)
        private int size;
        // Current capacity (how many elements can array contain without resizing)
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

        public DynamicArray(T[] source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("Source array was null");
            }
            if (source.Length > 0)
            {
                array = new T[source.Length];
                capacity = source.Length;
                Size = source.Length;
                for (int i = 0; i < source.Length; i++)
                {
                    array[i] = source[i];
                }
            }
            else
            {
                array = new T[1];
                capacity = 1;
                Size = 0;
            }
        }

        /// <summary>
        /// Number of elements contained in the dynamic array.
        /// </summary>
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

        /// <summary>
        /// Adds item to the end of the dynamic array, growing it if it is required.
        /// </summary>
        /// <param name="item">An item to add.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Add(T item)
        {
            if (Size == MAX_CAPACITY)
            {
                throw new InvalidOperationException("Cannot add element to dynamic array. Max capacity reached.");
            }
            if (capacity == Size)
            {
                Grow();
            }
            array[Size] = item;
            Size++;
        }

        /// <summary>
        /// Inserts item at a given index, shifting elements to the right.
        /// </summary>
        /// <param name="index">Index to insert to.</param>
        /// <param name="value">An item to insert.</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public void InsertAt(int index, T value)
        {
            if (Size == MAX_CAPACITY)
            {
                throw new InvalidOperationException("Cannot add element to dynamic array. Max capacity reached.");
            }
            ValidateIndex(index);
            if (Size + 1 > capacity)
            {
                Grow();
            }
            ShiftRightAtIndex(index);
            array[index] = value;
        }

        /// <summary>
        /// Removes element from the array and returns it. Elements right from the removed item
        /// are shifted to the left.
        /// </summary>
        /// <param name="index">Index of element to remove</param>
        /// <returns>Removed element.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T Remove(int index)
        {
            ValidateIndex(index);
            T returnValue = array[index];
            ShiftLeftAtIndex(index);
            return returnValue;
        }

        /// <summary>
        /// Clears the dynamic array.
        /// </summary>
        public void Clear()
        {
            Size = 0;
        }

        /// <summary>
        /// Ensures that dynamic array can fit a set number of element without resizing.
        /// </summary>
        /// <param name="newCapacity">New capacity of the dynamic array.</param>
        public void EnsureCapacity(int newCapacity)
        {
            Grow(newCapacity);
        }

        /// <summary>
        /// Grows array either by a factor of two if it is possible or to a max capacity.
        /// </summary>
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

        /// <summary>
        /// Grows array to specified capacity.
        /// </summary>
        /// <param name="newCapacity">New capacity of the dynamic array.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void Grow(int newCapacity)
        {
            if (newCapacity < capacity)
            {
                throw new InvalidOperationException("Cannot reduce capacity of the dynamic array.");
            }
            T[] newArray = new T[newCapacity];
            Array.Copy(array, newArray, Size);
            array = newArray;
            capacity = newCapacity;
        }

        /// <summary>
        /// Checks if index is in bounds of the dynamic array.
        /// </summary>
        /// <param name="index">Index of element in the dynamic array.</param>
        /// <returns>true, if index is in bounds, false otherwise</returns>
        private bool IsIndexValid(int index)
        {
            bool isInBounds = index >= 0 && index < Size;
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

        /// <summary>
        /// Shifts elements to the right of the element one position to the left.
        /// </summary>
        /// <param name="index">Index of element to shift from.</param>
        private void ShiftLeftAtIndex(int index)
        {
            for (int i = index; i < Size - 1; i++)
            {
                array[i] = array[i + 1];
            }
            Size--;
        }

        /// <summary>
        /// Shifts elements to the right of the element one position to the right.
        /// </summary>
        /// <param name="index">Index of element to shift from.</param>
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
