using System;
using Xunit;
using Algorithms.DataStructures;

namespace AlgorithmsUnitTests
{
    public class DynamicArrayUnitTests
    {
        [Theory]
        [InlineData(new int[] { }, new int[] { 1 }, 1)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2, 3 }, 3)]
        public void Test_OnAdd_ReturnsCorrectCollection(int[] initial, int[] expected, int toInsert)
        {
            // Arrange
            var sut = new DynamicArray<int>();
            foreach (var i in initial)
            {
                sut.Add(i);
            }
            // Act
            sut.Add(toInsert);
            // Assert
            Assert.Equal(expected.Length, sut.Size);
            for (int i = 0; i < sut.Size; i++)
            {
                Assert.Equal(expected[i], sut[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { 2, 3, 4 }, new int[] { 1, 2, 3, 4 }, 0, 1)]
        [InlineData(new int[] { 1, 3 }, new int[] { 1, 2, 3 }, 1, 2 )]
        [InlineData(new int[] { 1, 2, 4 }, new int[] { 1, 2, 3, 4 }, 2, 3)]
        public void Test_OnInsert_ReturnCorrectCollection(
            int[] initial, 
            int[] expected, 
            int insertionIndex, 
            int insertionValue)
        {
            // Arrange
            var sut = new DynamicArray<int>();
            foreach (var i in initial)
            {
                sut.Add(i);
            }
            // Act
            sut.InsertAt(insertionIndex, insertionValue);
            // Assert
            Assert.Equal(expected.Length, sut.Size);
            for (int i = 0; i < sut.Size; i++)
            {
                Assert.Equal(expected[i], sut[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { }, -1)]
        [InlineData(new int[] { }, 20)]
        [InlineData(new int[] { 1, 2, 3 }, 3)]
        [InlineData(new int[] { 1, 2, 3 }, 10)]
        [InlineData(new int[] { 1, 2, 3 }, -1)]
        public void Test_OnInsert_ThrowsOnInvalidIndex(int[] initial, int insertionIndex)
        {
            // Arrange
            int dummyValue = 0;
            var sut = new DynamicArray<int>();
            foreach (var i in initial)
            {
                sut.Add(i);
            }
            // Act
            // Assert
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                sut.InsertAt(insertionIndex, dummyValue);
            });
        }

        [Theory]
        [InlineData(new int[] { 1 }, new int[] { }, 0)]
        [InlineData(new int[] { 3, 1, 2, 4 }, new int[] { 3, 1, 2 }, 3)]
        [InlineData(new int[] { 3, 1, 2, 4 }, new int[] { 3, 1, 4 }, 2)]
        [InlineData(new int[] { 3, 1, 2, 4 }, new int[] { 1, 2, 4 }, 0)]
        public void Test_OnRemove_ReturnsCorrectCollection(int[] initial, int[] expected, int removalIndex)
        {
            // Arrange
            var sut = new DynamicArray<int>();
            foreach (var i in initial)
            {
                sut.Add(i);
            }
            // Act
            sut.Remove(removalIndex);
            // Assert
            Assert.Equal(expected.Length, sut.Size);
            for (int i = 0; i < sut.Size; i++)
            {
                Assert.Equal(expected[i], sut[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { }, 0)]
        [InlineData(new int[] { 1 }, 1)]
        [InlineData(new int[] { 3, 1, 2, 4 }, 10)]
        [InlineData(new int[] { 3, 1, 2, 4 }, 4)]
        [InlineData(new int[] { 3, 1, 2, 4 }, -1)]
        public void Test_OnRemove_ThrowsOnInvalidIndex(int[] initial, int removalIndex)
        {
            // Arrange
            var sut = new DynamicArray<int>();
            foreach (var i in initial)
            {
                sut.Add(i);
            }
            // Act
            // Assert
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                sut.Remove(removalIndex);
            });
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 1, 2 })]
        public void Test_OnClear_ReturnsCorrectSize(int[] initial)
        {
            // Arrange
            var sut = new DynamicArray<int>();
            foreach (var i in initial)
            {
                sut.Add(i);
            }
            // Act
            sut.Clear();
            // Assert
            Assert.Equal(0, sut.Size);
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1, 2, 3 })]
        public void Test_OnConstructionFromArray_ReturnsCorrectCollection(int[] source)
        {
            // Arrange
            // Act
            var sut = new DynamicArray<int>(source);
            // Assert
            for (int i = 0; i < source.Length; i++)
            {
                Assert.Equal(source[i], sut[i]);
            }
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 1, 2, 3 })]
        public void Test_OnConstructionFromArray_HasCorrectSize(int[] source)
        {
            // Arrange
            // Act
            var sut = new DynamicArray<int>(source);
            // Assert
            Assert.Equal(source.Length, sut.Size);
        }

        [Theory]
        [InlineData(null)]
        public void Test_OnConstructionFromArray_ThrowsOnNull(int[] source)
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var sut = new DynamicArray<int>(source);
            });
        }

        [Fact]
        public void Test_OnEnsuringCapacityPastMaximum_Throws()
        {
            // Arrange
            var sut = new DynamicArray<int>();
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(new Action(() => sut.EnsureCapacity(int.MaxValue)));
        }

        [Fact]
        public void Test_OnEnsuringCapacity_ThrowsOnReducingCapacity()
        {
            // Arrange
            var sut = new DynamicArray<int>();
            sut.EnsureCapacity(10);
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(new Action(() => sut.EnsureCapacity(5)));
        }

        [Fact]
        public void Test_OnEnsuringCapacity_ThrowsOnNegativeCapacity()
        {
            // Arrange
            var sut = new DynamicArray<int>();
            // Act
            // Assert
            Assert.ThrowsAny<Exception>(new Action(() => sut.EnsureCapacity(-1)));
        }
    }
}
