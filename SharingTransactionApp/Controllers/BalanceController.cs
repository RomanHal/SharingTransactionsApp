﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class BalanceController : ControllerBase
    {
        private readonly ISession _session;
        private readonly IBalanceService _balanceService;

        public BalanceController(ISession session,IBalanceService balanceService)
        {
            _session = session;
            _balanceService = balanceService;
        }
        [HttpGet]
        public IEnumerable<UserBalance> Get()
        {
            if (User.Claims.Where(c => c.Type == "name").FirstOrDefault() == null ) return new List<UserBalance>();
            var activeUser = User.Claims.Where(c => c.Type == "name").FirstOrDefault().Value;
            return _balanceService.GetBalances(activeUser);
        }
    }
}
