using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ShopController : Controller
    {
        private MyDbEshop db = new MyDbEshop();

        public IActionResult Index()
        {
            List<Produkt> Produkt = new List<Produkt>();

            if (db.Article.Count() > 0)
            {
                foreach (var item in db.Produkt)
                {
                    Produkt.Add(item);
                }
            }
            return View(Produkt);
        }

        
        public IActionResult Detail(int? id) 
        {
            Produkt product = db.Produkt.Find(id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }
    }
}
