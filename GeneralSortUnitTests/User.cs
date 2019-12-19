using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralSortUnitTests
{
    public class User : IComparable<User>, IEquatable<User>
    {
        public User(string firstName, string lastName, string address = "Szczecin", string posiotion = "admin", uint age = 21)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.Posiotion = posiotion;
            this.Age = age;
        }

        public string LastName { get; private set; }

        public string FirstName { get; private set; }

        public string Address { get; private set; }

        public string Posiotion { get; private set; }

        public uint Age { get; private set; }

        public int CompareTo(User other)
        {
            var list = new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(this.LastName, other.LastName),
                new Tuple<string, string>(this.FirstName, other.FirstName),
                new Tuple<string, string>(this.Address, other.Address),
                new Tuple<string, string>(this.Posiotion, other.Posiotion),
            };

            int value;
            foreach (var tuple in list)
            {
                value = tuple.Item1.CompareTo(tuple.Item2);
                if (value != 0)
                {
                    return value;
                }
            }

            return 0;
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(this.LastName, other.LastName) && 
            string.Equals(this.FirstName, other.FirstName)  &&
            string.Equals(this.Address, other.Address)  &&
            string.Equals(this.Posiotion, other.Posiotion)  &&
            string.Equals(this.Age, other.Age);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() ^ 
            this.LastName.GetHashCode() ^ 
            this.Address.GetHashCode() ^
            this.Age.GetHashCode() ^
            this.Posiotion.GetHashCode();
        }
    }
}