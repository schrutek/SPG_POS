using Spg.DependencyInjection.Tests.DataAccess;
using Spg.DependencyInjection.Tests.Interfaces;

namespace Spg.DependencyInjection.Tests.Services
{
    public class CustomerService
    {
        private ICustomerDataAccess _customerDataAccess;

        public CustomerService()
        {
            _customerDataAccess = new CustomerDataAccess();
        }

        public string GetCustomerName(int id)
        {
            return _customerDataAccess.GetCustomerName(id);
        }
    }
}
