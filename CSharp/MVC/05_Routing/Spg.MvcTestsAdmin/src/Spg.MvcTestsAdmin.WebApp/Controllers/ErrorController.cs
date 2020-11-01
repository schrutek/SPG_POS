using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Spg.MvcTestsAdmin.WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            return View();
        }
    }
}
