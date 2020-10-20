using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Demo.Interfaces
{
    public interface ICustomerDataAccess
    {
        string GetCustomerName(int id);
    }
}
