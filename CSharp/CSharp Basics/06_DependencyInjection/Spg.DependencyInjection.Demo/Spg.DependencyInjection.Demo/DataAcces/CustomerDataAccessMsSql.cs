using Spg.DependencyInjection.Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Demo.DataAcces
{
    public class CustomerDataAccessMsSql : ICustomerDataAccess
    {
        public string GetCustomerName(int id)
        {
            return $"Customername für ID: {id} von MSSQL-DB";
        }
    }
}
