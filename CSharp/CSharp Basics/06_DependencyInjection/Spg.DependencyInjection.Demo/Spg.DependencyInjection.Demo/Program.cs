using Spg.DependencyInjection.Demo.DataAcces;
using Spg.DependencyInjection.Demo.Services;
using System;

namespace Spg.DependencyInjection.Demo
{
    public class Program
    {
        private CustomerService _customerService;

        public Program()
        {
            _customerService = new CustomerService(new CustomerDataAccessMsSql());
        }

        public string GetCustomerNameFromService()
        {
            return _customerService.GetCustomerNameFromDb(12);
        }

        public static void Main(string[] args)
        {
            Program client = new Program();
            Console.WriteLine(client.GetCustomerNameFromService());
        }
    }
}
