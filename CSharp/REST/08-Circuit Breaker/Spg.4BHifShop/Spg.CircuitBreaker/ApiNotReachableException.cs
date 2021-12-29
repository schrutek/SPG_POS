using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public class ApiNotReachableException : Exception
    {
        public ApiNotReachableException()
            : base()
        { }

        public ApiNotReachableException(string message)
            : base(message)
        { }

        public ApiNotReachableException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
