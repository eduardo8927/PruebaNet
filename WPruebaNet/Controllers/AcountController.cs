using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WPruebaNet.Controllers
{
    public class AcountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            if (HttpContext.Request.Cookies.Count > 0)
            {
                var siteCookies = HttpContext.Request.Cookies.Where(c => c.Key.Contains(".AspNetCore.") || c.Key.Contains("UserLoginCookie"));
                foreach (var cookie in siteCookies)
                {
                    HttpContext.Response.Cookies.Delete(cookie.Key);
                }
            }
            HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Acount");
        }
    }
}
