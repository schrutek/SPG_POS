using Spg.DependencyInjection.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Demo.DataAcces
{
    public class CustomerDataAccessOracle : ICustomerDataAccess
    {
        public string GetCustomerName(int id)
        {
            return $"Customername für ID: {id} von Oracle-DB";
        }
    }
}
