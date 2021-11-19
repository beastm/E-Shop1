using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ContactController : Controller
    {
        private MyDbEshop db = new MyDbEshop();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostComment(Data.Feedback  feedback) 
        {
            if (feedback != null)
            {
                db.Feedback.Add(feedback);
                db.SaveChanges();
                return View();
            }

            return NotFound();
        }
    }
}
