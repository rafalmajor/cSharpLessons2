using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LinqTest
{
    public class LinqUnitTest
    {
        private readonly User[] randomUsers =
        {
            new User("Jan", "Nowak", "Szczecin", "dev", 22),
            new User("Zbigniew", "Wójcik", "Stargard", "admin", 23),
            new User("Stanisław", "Nowak", "Szczecin", "dev", 26),
            new User("Henryk", "Kamiński", "Gorzów", "admin", 28),
            new User("Andrzej", "Nowak", "Stargard", "dev", 20),
            new User("Józef", "Kowalski", "Szczecin", "admin", 50),
            new User("Jerzy", "Wiśniewski", "Stargard", "dev", 12),
            new User("Tadeusz", "Kowalski", "Gorzów", "admin", 24),
            new User("Krzysztof", "Kowalczyk", "Stargard", "dev", 22),
        };

        [Fact]
        public void WhereTest()
        {
            var selectedUsers1 = this.randomUsers.Where(user => user.LastName == "Nowak");

            var selectedUsers2 = from user in this.randomUsers
                                 where user.LastName == "Nowak"
                                 select user;

            Assert.Equal(selectedUsers1.ToArray(), selectedUsers2.ToArray());
        }

        [Fact]
        public void GroupTest()
        {
            var groupUsers1 = this.randomUsers.GroupBy(user => user.LastName);
            var groupUsers2 = from user in this.randomUsers
                              group user by user.LastName;

            Assert.Equal(groupUsers1.Select(u => u.Key).ToArray(), groupUsers2.Select(u => u.Key).ToArray());
        }

        [Fact]
        public void OrderByTest()
        {
            var orderUser1 = this.randomUsers.OrderBy(user => user.LastName).ThenBy(user => user.FirstName);
            var orderUser2 = from user in this.randomUsers
                             orderby user.LastName, user.FirstName
                             select user;

            Assert.Equal(orderUser1.ToArray(), orderUser2.ToArray());
        }

        [Fact]
        public void SelectTest()
        {
            var select1 = this.randomUsers.Select(user => $"{user.FirstName} {user.LastName}, age{user.Age}");
            var select2 = from user in this.randomUsers
                          select $"{user.FirstName} {user.LastName}, age{user.Age}";

            Assert.Equal(select1.ToArray(), select2.ToArray());
        }

        [Fact]
        public void SelectTwoTest()
        {
            var select1 = this.randomUsers.Select(user => new Cat(user.LastName));
            var select2 = from user in this.randomUsers
                          select new Cat(user.LastName);

            Assert.Equal(select1.ToArray(), select2.ToArray());
        }

        [Fact]
        public void MinTest()
        {
            var min1 = this.randomUsers.Min(user => user.Age);
            var min2 = (from user in this.randomUsers
                        select user.Age).Min();

            Assert.Equal(min1, min2);
        }

        [Fact]
        public void MaxTest()
        {
            var max1 = this.randomUsers.Max(user => user.Age);
            var max2 = (from user in this.randomUsers
                        select user.Age).Max();

            Assert.Equal(max1, max2);
        }

        [Fact]
        public void SumTest()
        {
            var sum1 = this.randomUsers.Sum(user => user.Age);
            var sum2 = (from user in this.randomUsers
                        select user.Age).Sum();

            Assert.Equal(sum1, sum2);
        }

        [Fact]
        public void SelectManyTest()
        {
            var cats = new List<Cat>
                       {
                           new Cat("Acat", new[] { new User("Jan", "Nowak"), new User("Stanisław", "Nowak") }),
                           new Cat("Bcat", new[] { new User("Jan", "Nowak") }),
                           new Cat("Ccat", new[] { new User("Tadeusz", "Kowalski"), new User("Krzysztof", "Kowalczyk", "Stargard") }),
                       };

            var result = cats.SelectMany(cat => cat.Users, (cat, user) => $"cat:'{cat.Name}' user:'{user.FirstName} {user.LastName}'");
            var expectedResult = new[]
                                 {
                                     "cat:'Acat' user:'Jan Nowak'",
                                     "cat:'Acat' user:'Stanisław Nowak'",
                                     "cat:'Bcat' user:'Jan Nowak'",
                                     "cat:'Ccat' user:'Tadeusz Kowalski'",
                                     "cat:'Ccat' user:'Krzysztof Kowalczyk'"
                                 };

            Assert.Equal(expectedResult, result.ToArray());
        }

        [Fact]
        public void AggegateTest()
        {
            var allNames = this.randomUsers.Aggregate(
                (result, currentUser) => result = new User(result.FirstName + " " + currentUser.FirstName, result.LastName + " " + currentUser.LastName));

            var expectedUser = new User("Jan Zbigniew Stanisław Henryk Andrzej Józef Jerzy Tadeusz Krzysztof", "Nowak Wójcik Nowak Kamiński Nowak Kowalski Wiśniewski Kowalski Kowalczyk");

            Assert.Equal(expectedUser.FirstName, allNames.FirstName);
            Assert.Equal(expectedUser.LastName, allNames.LastName);
        }

        [Fact]
        public void AnyTest()
        {
            var emptyList = new List<User>();
            Assert.False(emptyList.Any());
            Assert.True(this.randomUsers.Any());
            Assert.False(this.randomUsers.Any(user => user.Age > 100));
            Assert.True(this.randomUsers.Any(user => user.Age > 30));
        }

        [Fact]
        public void AllTest()
        {
            var emptyList = new List<User>();
            Assert.True(emptyList.All(user => user.Age > 10));
            Assert.True(this.randomUsers.All(user => user.Age > 10));
            Assert.False(this.randomUsers.All(user => user.Age > 20));
        }

        private class Cat
        {
            public Cat(string name, IEnumerable<User> users)
            {
                this.Name = name;
                this.Users = users;
            }

            public Cat(string ownerLastName)
            {
                this.OwnerLastName = ownerLastName;
            }

            private Cat(string name, string ownerLastName)
            {
                this.Name = name;
                this.OwnerLastName = ownerLastName;

            }
            public string Name { get; }

            public IEnumerable<User> Users { get; }

            public string OwnerLastName { get; }

            public override string ToString()
            {
                return $"Owner:{this.OwnerLastName}";
            }

            public bool Equals(Cat cat)
            {
                if (cat == null) return false;
                return cat.Name == this.Name &&
                cat.OwnerLastName == this.OwnerLastName;
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                if (!(obj is Cat)) return false;
                return this.Equals((Cat)obj);
            }

            public override int GetHashCode()
            {
                int value = 123456;
                if (this.Name != null)
                    value ^= this.Name.GetHashCode();
                if (this.OwnerLastName != null)
                    value ^= this.OwnerLastName.GetHashCode();
                return value;
            }
        }
    }
}
