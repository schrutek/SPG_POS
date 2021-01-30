using Spg.DependencyInjection.Tests.DataAccess;
using Spg.DependencyInjection.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Tests.Services
{
    public class CustomerService
    {
        private ICustomerDataAccess _customerDataAccess;

        public CustomerService(ICustomerDataAccess customerDataAccess)
        {
            _customerDataAccess = customerDataAccess;
        }

        public string GetCustomerName(int id)
        {
            return _customerDataAccess.GetCustomerName(id);
        }
    }
}
