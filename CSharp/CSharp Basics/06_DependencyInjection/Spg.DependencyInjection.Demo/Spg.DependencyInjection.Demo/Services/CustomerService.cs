using Spg.DependencyInjection.Demo.DataAcces;
using Spg.DependencyInjection.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Demo.Services
{
    public class CustomerService
    {
        private ICustomerDataAccess _dataAccess;
        
        public CustomerService(ICustomerDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            //_dataAccess = new CustomerDataAccessMsSql();
        }
        
        public string GetCustomerNameFromDb(int id)
        {
            return _dataAccess.GetCustomerName(id);
        }
    }
}
