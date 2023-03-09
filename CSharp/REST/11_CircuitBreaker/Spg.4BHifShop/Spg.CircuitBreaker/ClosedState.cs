using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public class ClosedState : CircuitBreakerState
    {
        public ClosedState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker)
        {
            circuitBreaker.ResetFailureCount();
        }

        /// <summary>
        /// Circuit ist Closed, bei einem Fehler muss reagiert werden:
        /// State => Open
        /// </summary>
        /// <param name="e"></param>
        public override void ActOnException(Exception e)
        {
            base.ActOnException(e);
            if (_circuitBreaker.IsThresholdReached())
            {
                _circuitBreaker.MoveToOpenState();
            }
        }
    }
}
