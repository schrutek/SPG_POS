using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Spg.CoocieAuthentication.Mvc.Extensions;
using Spg.CoocieAuthentication.Mvc.Models;

namespace Spg.CoocieAuthentication.Mvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Löscht das existierende Cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                // Lediglich zu Demonstrationszwecken wird hier der User nur 
                // mittels E-Mail-Adresse (schrutek@spengergasse.at") validiert. 
                // Das Kennwort wird ignoriert
                var user = await AuthenticateUser(model.Email, model.FullName);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                _logger.LogInformation("User {Email} logged in at {Time}.",
                    user.Email, DateTime.UtcNow);

                if (!String.IsNullOrEmpty(returnUrl))
                {
                    //return RedirectToLocal(returnUrl); 
                    return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }
                else
                {
                    return LocalRedirect("/Home/Index");
                }
            }
            // Im Fehlerfall wird wieder die View  mit leeren Textfeldern retuniert.
            return View();
        }

        private async Task<ApplicationUser> AuthenticateUser(string email, string password)
        {
            // For demonstration purposes, authenticate a user
            // with a static email address. Ignore the password.
            // Assume that checking the database takes 500ms

            await Task.Delay(500);

            if (email == "schrutek@spengergasse.at")
            {
                return new ApplicationUser()
                {
                    Email = "schrutek@spengergasse.at",
                    FullName = "Martin Schrutek"
                };
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User {Name} logged out at {Time}.", User.Identity.Name, DateTime.UtcNow);

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect("/Account/SignedOut");

        }

        [HttpGet()]
        public IActionResult SignedOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                // Redirect zu Home wenn der User nicht authentifiziert ist
                return RedirectToPage("/Index");
            }
            return View();
        }

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}
    }
}
