using Crypto.API;
using GameLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Ditech.Portal.NET.Controllers
{
    public class CryptoController : Controller
    {
        private readonly ILogger<CryptoController> ilogger;
        private readonly IMailService mailService;

        public CryptoController(ILogger<CryptoController> ilogger, IMailService mailService)
        {
            this.ilogger = ilogger;
            this.mailService = mailService; 
        }

        public IActionResult Index()
        {
            MailRequest mailRequest = new MailRequest { Attachments = null, Body = "", Subject = "buy it", ToEmail = "nikunjrathod3011@gmail.com" };
            var webClient = new WebClient();
            var json = webClient.DownloadString("https://api.coingecko.com/api/v3/coins/markets?vs_currency=cad&order=market_cap_desc&per_page=10&page=1&sparkline=false");
            var operations = JsonConvert.DeserializeObject<List<CryptoViewModel>>(json);
            var checkEthereum = operations.Where(o => o.current_price <= o.low_24h + 200).ToList();
            if (checkEthereum.Count == 1)
            {
                ilogger.LogInformation("abcd");
                mailService.SendEmailAsync(mailRequest.Body=);
            }
            HttpContext.Response.Headers.Add("refresh", "120; url=" + Url.Action("Index"));
            return View(operations.ToList());
        }
    }
}
