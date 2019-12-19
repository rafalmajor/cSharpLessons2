using System;
using Xunit;

namespace SortUnitTest
{
    public class SortUnitTest
    {
        private readonly int[] randomItems = { 16, 4, 19, 5, 15, 3, 18, 9, 11, 7, 20, 14, 1, 12, 8, 10, 2, 6, 13, 17 };

        private readonly int[] sortedItems = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };


        [Fact]
        public void BubbleSortTest()
        {
            var sorted = BubbleSort.Sort(this.randomItems);

            Assert.Equal(this.sortedItems, sorted);
        }

        [Fact]
        public void InsertSortTest()
        {
            var sorted = InsertSort.Sort(this.randomItems);
            Assert.Equal(this.sortedItems, sorted);
        }

        [Fact]
        public void QuickSortTest()
        {
            var sorted = QuickSort.Sort(this.randomItems);
            Assert.Equal(this.sortedItems, sorted);
        }
    }
}
