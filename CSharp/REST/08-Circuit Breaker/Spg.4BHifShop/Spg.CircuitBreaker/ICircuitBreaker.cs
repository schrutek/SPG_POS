using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public interface ICircuitBreaker
    {
        event Action<object, CircuitBreakerState> OnStateChange;

        int Failures { get; }
        int Threshold { get; }
        TimeSpan Timeout { get; }
        bool IsClosed { get; }
        bool IsOpen { get; }
        bool IsHalfOpen { get; }
        bool IsThresholdReached();

        Exception GetExceptionFromLastAttemptCall();
        CircuitBreaker AttemptApiCall<TException>(Action protectedCode)
            where TException : Exception, new();

        void Close();
        void Open();
    }
}
