using System;
using System.Collections.Generic;

namespace SPG.Comparer
{
    public class Program
    {
        private static List<Customer> _customers;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            _customers = new List<Customer>()
            {
                new Customer() { Id = 1, Name = "C1" },
                new Customer() { Id = 3, Name = "C3" },
                new Customer() { Id = 6, Name = "C6" },
                new Customer() { Id = 2, Name = "C2" },
                new Customer() { Id = 4, Name = "C4" },
                new Customer() { Id = 5, Name = "C5" },
            };

            Console.WriteLine($"UNSORTIERT: ***************");
            foreach (Customer c in _customers)
            {
                Console.WriteLine($"ID={c.Id} ; Name={c.Name}");
            }

            _customers.Sort();

            //Comparison<Customer> customerComparer = new Comparison<Customer>(CompareCustomer);

            //_customers.Sort(customerComparer);

            //_customers.Sort(delegate (Customer x, Customer y) { return x.Id.CompareTo(y.Id); });

            Console.WriteLine($"SORTIERT: *****************");
            foreach (Customer c in _customers)
            {
                Console.WriteLine($"ID={c.Id} ; Name={c.Name}");
            }

            Console.ReadLine();
        }

        private static int CompareCustomer(Customer x, Customer y)
        {
            int retVal = x.Id.CompareTo(y.Id);
            return retVal;
        }
    }

    class Position : IEquatable<Position>
    {
        public bool Equals(Position other)
        {
            throw new NotImplementedException();
        }
    }

    public class Customer : IComparable<Customer>, IComparable, IEquatable<Customer>
    {
        public int Id { get; set; }

        public string Name{ get; set; }

        public int CompareTo(Customer other)
        {
            return Id.CompareTo(other?.Id);
        }

        public int CompareTo(object obj)
        {
            return CompareTo((Customer)obj);
        }

        public bool Equals(Customer other)
        {
            return Id.Equals(other?.Id);
        }

        public static bool operator <(Customer c1, Customer c2)
        {
            return c1.CompareTo(c2) < 0;
        }

        public static bool operator >(Customer c1, Customer c2)
        {
            return c1.CompareTo(c2) > 0;
        }

        public static bool operator ==(Customer c1, Customer c2)
        {
            return c1.CompareTo(c2) == 0;
        }

        public static bool operator !=(Customer c1, Customer c2)
        {
            return c1.CompareTo(c2) != 0;
        }
    }
}
