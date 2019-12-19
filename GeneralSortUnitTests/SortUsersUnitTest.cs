using System;
using Xunit;

namespace GeneralSortUnitTests
{
    public class SortUsersUnitTest
    {
        private readonly User[] randomUsers =
        {
            new User("Jan", "Nowak"),
            new User("Zbigniew", "Wójcik"),
            new User("Stanisław", "Nowak"),
            new User("Henryk", "Kamiński"),
            new User("Andrzej", "Nowak"),
            new User("Józef", "Kowalski"),
            new User("Jerzy", "Wiśniewski"),
            new User("Tadeusz", "Kowalski"),
            new User("Krzysztof", "Kowalczyk"),
        };

        private readonly User[] sortedUsers =
        {
            new User("Henryk", "Kamiński"),
            new User("Krzysztof", "Kowalczyk"),
            new User("Józef", "Kowalski"),
            new User("Tadeusz", "Kowalski"),
            new User("Andrzej", "Nowak"),
            new User("Jan", "Nowak"),
            new User("Stanisław", "Nowak"),
            new User("Jerzy", "Wiśniewski"),
            new User("Zbigniew", "Wójcik"),
        };

        [Fact]
        public void BubbleSortTest()
        {
            Assert.Equal(this.sortedUsers, BubbleSort.Sort(this.randomUsers));
        }

        [Fact]
        public void InsertSortTest()
        {
            Assert.Equal(this.sortedUsers, InsertSort.Sort(this.randomUsers));
        }

        [Fact]
        public void QuickSortTest()
        {
            Assert.Equal(this.sortedUsers, QuickSort.Sort(this.randomUsers));
        }
    }
}
