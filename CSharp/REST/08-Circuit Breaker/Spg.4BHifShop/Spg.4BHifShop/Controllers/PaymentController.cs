using Microsoft.AspNetCore.Mvc;
using Spg._4BHifShop.Services;
using Spg.CircuitBreaker;

namespace Spg._4BHifShop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ICircuitBreaker _circuitBreaker;
        private readonly PaymentApiWebService _paymentApiWebService;

        /// <summary>
        /// Constructior-Injection
        /// </summary>
        /// <param name="circuitBreaker"></param>
        public PaymentController(ICircuitBreaker circuitBreaker, 
            PaymentApiWebService paymentApiWebService)
        {
            _circuitBreaker = circuitBreaker;
            _paymentApiWebService = paymentApiWebService;
        }

        public IActionResult CallPayment()
        {
            if (_circuitBreaker.AttemptApiCall<ApiNotReachableException>(_paymentApiWebService.DoPaymentApiCall).IsClosed)
            {
                Console.WriteLine("Alles OK :) Requests werden ausgeführt!");
            }
            else
            {
                Console.WriteLine("API scheint down zu sein :(");
            }
            return View();
        }
    }
}
