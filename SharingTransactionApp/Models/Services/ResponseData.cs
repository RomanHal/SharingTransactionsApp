using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharingTransactionApp.Models.Services
{
    public class ResponseData<T>
    {
        public T Data { get; set; }
    }
}
