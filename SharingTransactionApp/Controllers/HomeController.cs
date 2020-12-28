using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp/build/index.html");
            
            return PhysicalFile(path, "text/html");
        }
    }
}
