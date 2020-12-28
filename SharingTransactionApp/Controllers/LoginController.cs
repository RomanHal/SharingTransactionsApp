using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Controllers
{
    [ApiController]
    [Authorize]
    public class LoginController : ControllerBase
    {
        [Route("api/login")]
        [HttpGet]
        public IActionResult Login()
        {
            return Redirect("/");
        }
        [Route("api/logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            //return SignOut();
            //return new SignOutResult(new[] { "test123", "oidc" });
            return SignOut("test123", "oidc");
        }
    }
}
