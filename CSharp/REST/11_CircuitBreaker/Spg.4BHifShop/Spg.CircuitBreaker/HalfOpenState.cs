using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public class HalfOpenState : CircuitBreakerState
    {
        public HalfOpenState(CircuitBreaker circuitBreaker)
            : base(circuitBreaker) { }

        public override void ActOnException(Exception e)
        {
            base.ActOnException(e);
            _circuitBreaker.MoveToOpenState();
        }

        /// <summary>
        /// Nur wenn State "HalfOpen" ist, kann sich der State auf Closed ändern.
        /// Bei Open => Requests werden sowieso verweigert (keine State-Änderung).
        /// Wenn Closed => Alles Gut, Requests gehen sowieso durch (keine State-Änderung).
        /// Darum ist diese Methode nur hieer überschrieben.
        /// </summary>
        public override void ProtectedCodeExecuted()
        {
            base.ProtectedCodeExecuted();
            _circuitBreaker.MoveToClosedState();
        }
    }
}
