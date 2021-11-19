using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Eshop.Models;
using Eshop.Data;

namespace Eshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyDbEshop db = new MyDbEshop();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Show Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<Produkt> Produkt = new List<Produkt>();

            if (db.Produkt.Count() > 0)
            {
                foreach (var item in db.Produkt)
                {
                    Produkt.Add(item);
                }
            }
            return View(Produkt);
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
