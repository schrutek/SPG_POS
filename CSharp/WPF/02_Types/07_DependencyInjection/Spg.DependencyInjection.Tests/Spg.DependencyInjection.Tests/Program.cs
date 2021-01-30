using Spg.DependencyInjection.Tests.DataAccess;
using Spg.DependencyInjection.Tests.Services;
using System;

namespace Spg.DependencyInjection.Tests
{
    public class Program
    {
        CustomerService _customerService;

        public Program()
        {
            _customerService = new CustomerService(new CustomerDataAccess());
        }

        public void ProcessCustomerName()
        {
            Console.WriteLine(_customerService.GetCustomerName(12));
        }

        public static void Main(string[] args)
        {
            Program program = new Program();
            program.ProcessCustomerName();
        }
    }
}
