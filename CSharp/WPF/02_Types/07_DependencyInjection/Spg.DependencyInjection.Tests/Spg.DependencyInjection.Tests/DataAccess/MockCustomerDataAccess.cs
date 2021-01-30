using Spg.DependencyInjection.Tests.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Tests.DataAccess
{
    public class MockCustomerDataAccess : ICustomerDataAccess
    {
        public string GetCustomerName(int id)
        {
            return $"Dummy Customer Name with id {id} (from Mock)";
        }
    }
}
