using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Data;
using Eshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers
{
    public class ProductsController : Controller
    {
        private MyDbEshop db = new MyDbEshop();

        /// <summary>
        /// Show Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int? catId, SortProdukt sortProdukt)
        {
            List<Produkt> produkt = new List<Produkt>();
            if (catId == null)
            {
               

                if (db.Article.Count() > 0)
                {
                    foreach (var item in db.Produkt)
                    {
                        produkt.Add(item);
                    }
                }
            }
            else {
                var listCat = db.ProdCat.Where(x => x.CatId == catId).ToList();

                if (listCat != null)
                {
                    foreach (var item in listCat)
                    {
                        Produkt pr = db.Produkt.Find(item.ProduktId);
                        produkt.Add(pr);
                    } 
                }

            }

            List<Data.Category> categories = db.Category.ToList();
            ViewData["CategorieList"] = categories;

            var vendorList = db.Produkt.Select(x => x.Vendor).ToList();
            var colorList = db.Produkt.Select(x => x.Color).ToList();
            var tagList = db.Produkt.Select(x => x.Tags).ToList();
            var sizeList = db.Produkt.Select(x => x.Size).ToList();

            ViewData["VendorList"] = vendorList;
            ViewData["ColorList"] = colorList;
            ViewData["TagList"] = tagList;
            ViewData["SizeList"] = sizeList;


            if (sortProdukt.Vendors != null  || sortProdukt.Colors != null  || sortProdukt.Tags != null || sortProdukt.Sizes != null)
            {

                List<Produkt> pr = new List<Produkt>();
                pr.AddRange(produkt);
                // vendor
                if (sortProdukt.Vendors != null)
                {
                    List<Produkt> vendoredList = new List<Produkt>();
                    foreach (var item in sortProdukt.Vendors)
                    {
                        var tmpP = db.Produkt.Where(x => x.Vendor == item).ToList();
                        vendoredList.AddRange(tmpP);
                    }
                    pr.Clear();
                    pr.AddRange(vendoredList);

                }

                // color
                if (sortProdukt.Colors != null)
                {
                    List<Produkt> coloredList = new List<Produkt>();
                    foreach (var item in sortProdukt.Colors)
                    {
                        var coloredProduct = pr.Where(x => x.Color == item).ToList();
                        coloredList.AddRange(coloredProduct);
                    }

                    pr.Clear();
                    pr.AddRange(coloredList);
                }

                // Tag
                if (sortProdukt.Tags != null)
                {
                    List<Produkt> tagedList = new List<Produkt>();
                    foreach (var item in sortProdukt.Tags)
                    {
                        var taggedProduct = pr.Where(x => x.Tags == item).ToList();
                        tagedList.AddRange(taggedProduct);
                    }

                    pr.Clear();
                    pr.AddRange(tagedList);
                }

                // Size
                if (sortProdukt.Sizes != null)
                {
                    List<Produkt> sizedList = new List<Produkt>();
                    foreach (var item in sortProdukt.Sizes)
                    {
                        var sizedProduct = pr.Where(x => x.Size == item).ToList();
                        sizedList.AddRange(sizedProduct);
                    }

                    pr.Clear();
                    pr.AddRange(sizedList);
                }



                produkt.Clear();
                produkt.AddRange(pr);

            }


            return View(produkt);


           
        }

        /// <summary>
        /// Show Detail
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(int? id) 
        {
            return View();
        }
    }
}
