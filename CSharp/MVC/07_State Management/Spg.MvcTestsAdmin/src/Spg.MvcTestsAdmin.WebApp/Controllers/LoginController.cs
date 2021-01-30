using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spg.MvcTestsAdmin.WebApp.Models;

namespace Spg.MvcTestsAdmin.WebApp.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController()]
    public class LoginController : Controller
    {
        [HttpGet()]
        public IActionResult Index()
        {
            string userName = Request.Cookies["UserName"];

            return View("Index", new Login() { UserName = userName });
        }

        [HttpPost()]
        public IActionResult Index([FromForm] Login model)
        {
            string userName = model.UserName;

            if (userName == "xy")
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Append("UserName", userName, option);

                return View("Index", new Login() { UserName = userName });
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet()]
        public IActionResult RemoveCookie()
        {
            Response.Cookies.Delete("UserName");
            return View("Index");
        }
    }
}
