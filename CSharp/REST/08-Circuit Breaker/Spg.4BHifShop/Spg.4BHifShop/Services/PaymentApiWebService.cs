using Spg.CircuitBreaker;

namespace Spg._4BHifShop.Services
{
    /// <summary>
    /// Simuliert einfach nur eine fehlerhafte, instabile API.
    /// </summary>
    public class PaymentApiWebService
    {
        private int _calls = 0;

        public void DoPaymentApiCall()
        {
            _calls++;
            Console.WriteLine("Payment API-Call wurde durchgeführt!");

            for (int i = 0; i < 1000; i++)
            { }

            if (_calls % 10 == 0)
            {
                throw new ApiNotReachableException("Payment API is temporarily down! Please wait...");
            }
        }
    }
}
