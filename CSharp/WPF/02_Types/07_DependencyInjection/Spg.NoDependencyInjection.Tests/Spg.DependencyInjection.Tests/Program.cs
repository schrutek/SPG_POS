using Spg.DependencyInjection.Tests.Services;
using System;

namespace Spg.DependencyInjection.Tests
{
    public class Program
    {
        CustomerService _cusomerService;

        public Program()
        {
            _cusomerService = new CustomerService();
        }

        public void ProcessCustomerName()
        {
            Console.WriteLine(_cusomerService.GetCustomerName(12));
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.ProcessCustomerName();
        }
    }
}
