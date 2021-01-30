using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Spg.EqualsExercise.App
{
    public class PhoneNr : IEquatable<PhoneNr>, IComparable<PhoneNr>, IComparable
    {
        public long Vorwahl { get; }

        public long Telefonnummer { get; }

        public PhoneNr(long vorwahl, long telefonnummer)
        {
            Vorwahl = vorwahl;
            Telefonnummer = telefonnummer;
        }

        public int CompareTo([AllowNull] PhoneNr other)
        {
            int result = this.Vorwahl.CompareTo(other?.Vorwahl);
            if (result == 0)
            {
                return this.Telefonnummer.CompareTo(other?.Telefonnummer);
            }
            return result;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as PhoneNr);
        }

        public bool Equals([AllowNull] PhoneNr other)
        {
            return this.Vorwahl.Equals(other?.Vorwahl) 
                && this.Telefonnummer.Equals(other?.Telefonnummer);
        }

        public override bool Equals(object other)
        {
            return Equals(other as PhoneNr);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Vorwahl, Telefonnummer);
        }

        public static bool operator <(PhoneNr p1, PhoneNr p2) => p1.CompareTo(p2) < 0;
        public static bool operator <=(PhoneNr p1, PhoneNr p2) => p1.CompareTo(p2) <= 0;
        public static bool operator >(PhoneNr p1, PhoneNr p2) => p1.CompareTo(p2) > 0;
        public static bool operator >=(PhoneNr p1, PhoneNr p2) => p1.CompareTo(p2) >= 0;
    }
}
