using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SharingTransactionApp.Models;
using SharingTransactionApp.Models.Inerfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMongoService _service;

        public ImageController(IMongoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ImageJson Get(Guid id)
        {
            
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            var transaction = _service.TransactionCollection.Find(tr => tr.File == id).FirstOrDefault();
            if (transaction is null) return null;
           // if (transaction.Creator.Name != activeUser || transaction.Shareholders.Any(sh => sh.Person.Name == activeUser)) return null;
            return _service.ImageJsonCollection.Find(im => im.Id == id).FirstOrDefault();
        }
    }
}
