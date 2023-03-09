using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.CircuitBreaker
{
    public class CircuitBreaker : ICircuitBreaker
    {
        private readonly object _monitor = new object();
        private CircuitBreakerState _state;
        public event Action<object, CircuitBreakerState> OnStateChange;

        public CircuitBreaker(int threshold, TimeSpan timeout)
        {
            if (threshold < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(threshold), @"Threshold should be greater than 0");
            }

            if (timeout.TotalMilliseconds < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout), @"Timeout should be greater than 0");
            }

            Threshold = threshold;
            Timeout = timeout;
            MoveToClosedState();
        }

        public int Failures { get; private set; }
        public int Threshold { get; }
        public TimeSpan Timeout { get; }

        public bool IsClosed => _state.Update() is ClosedState;
        public bool IsOpen => _state.Update() is OpenState;
        public bool IsHalfOpen => _state.Update() is HalfOpenState;

        internal CircuitBreakerState MoveToClosedState()
        {
            _state = new ClosedState(this);
            NotifyStateChange(_state);
            return _state;
        }

        internal CircuitBreakerState MoveToOpenState()
        {
            _state = new OpenState(this);
            NotifyStateChange(_state);
            return _state;
        }

        internal CircuitBreakerState MoveToHalfOpenState()
        {
            _state = new HalfOpenState(this);
            NotifyStateChange(_state);
            return _state;
        }

        internal void IncreaseFailureCount()
        {
            Failures++;
        }

        internal void ResetFailureCount()
        {
            Failures = 0;
        }

        public bool IsThresholdReached()
        {
            return Failures >= Threshold;
        }

        private Exception _exceptionFromLastAttemptCall = default!;

        public Exception GetExceptionFromLastAttemptCall()
        {
            return _exceptionFromLastAttemptCall;
        }

        /// <summary>
        /// Folgendermaßen verwenden:
        /// myCircuitBreaker.AttemptCall(() => { yourCode(); } ).IsClosed ? "AllFine" : "Something wrong";
        /// </summary>
        /// <typeparam name="TException">z.B.: ApiUnreachableException(...)</typeparam>
        /// <param name="protectedCode"></param>
        /// <returns></returns>
        public CircuitBreaker AttemptApiCall<TException>(Action protectedCode)
            where TException : Exception, new()
        {
            _exceptionFromLastAttemptCall = default!;
            lock (_monitor)
            {
                _state.ProtectedCodeIsExecuting();
                if (_state is OpenState)
                {
                    return this; // Code wird nicht weiter ausgeführt
                }
            }
            try
            {
                protectedCode();
            }
            catch (TException e)
            {
                _exceptionFromLastAttemptCall = e;
                lock (_monitor)
                {
                    _state.ActOnException(e);
                }
                return this; // Code wird nicht weiter ausgeführt
            }

            lock (_monitor)
            {
                _state.ProtectedCodeExecuted();
            }
            return this;
        }

        public void Close()
        {
            lock (_monitor)
            {
                MoveToClosedState();
            }
        }

        public void Open()
        {
            lock (_monitor)
            {
                MoveToOpenState();
            }
        }

        /// <summary>
        /// Call the stage change event
        /// </summary>
        /// <param name="state"></param>
        private void NotifyStateChange(CircuitBreakerState state)
        {
            OnStateChange?.Invoke(this, state);
        }
    }
}