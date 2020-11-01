using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.MvcTestsAdmin.Service.Services
{
    public class ServiceLayerException : Exception
    {
        public ServiceLayerException()
            : base()
        { }

        public ServiceLayerException(string message)
            : base(message)
        { }

        public ServiceLayerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
