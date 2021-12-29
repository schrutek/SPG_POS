using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public class OpenState : CircuitBreakerState
    {
        private readonly DateTime _openDateTime;

        public OpenState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            _openDateTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Es muss überprüft werden, ob eine Timeout abgelaufen ist um in den
        /// HalfOpenState zu wechseln [siehe Update()]
        /// </summary>
        /// <returns></returns>
        public override CircuitBreaker ProtectedCodeIsExecuting()
        {
            base.ProtectedCodeIsExecuting();
            Update();
            return _circuitBreaker;

        }

        public override CircuitBreakerState Update()
        {
            base.Update();
            if (DateTime.UtcNow >= _openDateTime + _circuitBreaker.Timeout)
            {
                return _circuitBreaker.MoveToHalfOpenState();
            }
            return this;
        }
    }
}
