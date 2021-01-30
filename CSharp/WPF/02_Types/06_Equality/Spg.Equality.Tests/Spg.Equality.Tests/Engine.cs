using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Spg.Equality.Tests
{
    public class Engine : IEquatable<Engine>, IComparable, IComparable<Engine>
    {
        public int EngineSize { get;}

        public int RotationSpeed { get; }

        public string Brand { get; }

        public Engine(string brand, int engineSize, int rotationSpeed)
        {
            Brand = brand;
            EngineSize = engineSize;
            RotationSpeed = rotationSpeed;
        }

        public bool Equals([AllowNull] Engine other)
        {
            return this.EngineSize.Equals(other.EngineSize) 
                && this.RotationSpeed.Equals(other.RotationSpeed)
                && this.Brand.Equals(other.Brand);
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Engine);
        }

        public int CompareTo([AllowNull] Engine other)
        {
            return this.EngineSize.CompareTo(other.EngineSize);
        }

        //Unterschiedliche HashCoses bedeuten unterschiedliche Objekte
        //Gleiche Hashcodes bedeuten meist gleiche Objekte
        public override int GetHashCode()
        {
            // Alternativ:
            // return this.EngineSize.GetHashCode() ^ this.RotationSpeed.GetHashCode() ^ this.Brand.GetHashCode();

            // Besser:
            return HashCode.Combine(EngineSize, RotationSpeed, Brand);
        }

        public static bool operator <(Engine e1, Engine e2) => e1.CompareTo(e2) < 0;

        public static bool operator >(Engine e1, Engine e2) => e1.CompareTo(e2) > 0;

        public static bool operator <=(Engine e1, Engine e2) => e1.CompareTo(e2) <= 0;

        public static bool operator >=(Engine e1, Engine e2) => e1.CompareTo(e2) >= 0;
    }
}
