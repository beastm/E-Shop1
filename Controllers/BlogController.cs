using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Data;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class BlogController : Controller
    {
        private MyDbEshop db = new MyDbEshop();

        public IActionResult Index()
        {
            List<Article> articles = new List<Article>();

            if (db.Article.Count() > 0)
            {
                foreach (var item in db.Article)
                {
                    articles.Add(item);
                }
            }
            return View(articles);
        }

        /// <summary>
        /// Show detail of article // READ MORE btn
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(int? id)
        {
            Article article = db.Article.Find(id);
            if (article != null)
            {
                return View(article);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult PostComment(Data.Comment commentModel)
        {
            Data.Comment dataModel = new Data.Comment();
            if (commentModel != null)
            {
                dataModel.Username = commentModel.Username;
                dataModel.Email = commentModel.Email;
                dataModel.Message = commentModel.Message;

                db.Comment.Add(dataModel);
                db.SaveChanges();
            }

            return View();

        }

    }
}
