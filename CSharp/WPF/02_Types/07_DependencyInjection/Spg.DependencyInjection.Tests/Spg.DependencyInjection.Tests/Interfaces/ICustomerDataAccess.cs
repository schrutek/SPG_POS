using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.DependencyInjection.Tests.Interfaces
{
    public interface ICustomerDataAccess
    {
        string GetCustomerName(int id);
    }
}
