using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Spg.Equality.Tests
{
    public class Person : IComparable, IComparable<Person>, IEquatable<Person>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompareTo([AllowNull] Person other)
        {
            return (this.FirstName + this.LastName).CompareTo(other?.FirstName + other?.LastName);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((Person)obj);
        }

        public bool Equals([AllowNull] Person other)
        {
            return this.FirstName.Equals(other?.FirstName) && this.LastName.Equals(other?.LastName);
        }

        public override bool Equals(object obj)
        {
            return Equals((Person)obj);
        }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() ^ this.LastName.GetHashCode();
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public static bool operator ==(Person p1, Person p2) => p1.Equals(p2);
        public static bool operator !=(Person p1, Person p2) => !p1.Equals(p2);
    }
}
