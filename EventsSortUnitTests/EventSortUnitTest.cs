using System;
using System.Collections.Generic;
using Xunit;

namespace EventsSortUnitTests
{
    public class EventSortUnitTest
    {
        private readonly int[] randomItems = { 16, 4, 19, 5, 15, 3, 18, 9, 11, 7, 20, 14, 1, 12, 8, 10, 2, 6, 13, 17 };

        private readonly int[] sortedItems = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        
        [Fact]
        public void BubbleSortTest()
        {
            var bubbleSort = new BubbleSort();
            bubbleSort.Done += this.SortOnDone;
            bubbleSort.Input = this.randomItems;
            bubbleSort.Sort();
        }

        [Fact]
        public void InsertSortTest()
        {
            var insertSort = new InsertSort();
            insertSort.Done += (s, a) =>
            {
                Assert.Equal(this.sortedItems, ((ISort)s).Output);
            };
            insertSort.Input = this.randomItems;
            insertSort.Sort();
        }

        [Fact]
        public void QuickSortTest()
        {
            var quickSort = new QuickSort();
            quickSort.Done += delegate(object sender, EventArgs args)
            {
                Assert.Equal(this.sortedItems, ((ISort)sender).Output);
            };
            quickSort.Input = this.randomItems;
            quickSort.Sort();
        }

        [Fact]
        public void TestAll()
        {
            var sorts = new List<ISort> { new BubbleSort(), new InsertSort(), new QuickSort() };
            foreach (var sort in sorts)
            {
                sort.Done += this.SortOnDone;
            }

            foreach (var sort in sorts)
            {
                sort.Input = this.randomItems;
            }

            foreach (var sort in sorts)
            {
                sort.Sort();
            }
        }

        private void SortOnDone(object sender, EventArgs e)
        {
            Assert.Equal(this.sortedItems, ((ISort)sender).Output);
        }
    }
}
