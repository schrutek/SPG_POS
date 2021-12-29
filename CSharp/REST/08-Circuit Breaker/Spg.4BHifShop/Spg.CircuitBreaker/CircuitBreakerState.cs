using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public class CircuitBreakerState
    {
        protected readonly CircuitBreaker _circuitBreaker;

        protected CircuitBreakerState(CircuitBreaker circuitBreaker)
        {
            _circuitBreaker = circuitBreaker;
        }

        public virtual CircuitBreaker ProtectedCodeIsExecuting()
        {
            return _circuitBreaker;
        }

        public virtual void ProtectedCodeExecuted()
        { }

        public virtual void ActOnException(Exception e)
        {
            _circuitBreaker.IncreaseFailureCount();
        }

        public virtual CircuitBreakerState Update()
        {
            return this;
        }
    }
}