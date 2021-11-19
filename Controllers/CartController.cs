using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Models;
using Eshop.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Eshop.Controllers
{
    public class CartController : Controller
    {

        private MyDbEshop db = new MyDbEshop();

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index(int idBasket)
        {
            var myBasket =  db.Basket.Find(idBasket);

            if (myBasket != null)
            {
                Dictionary<int, string> productAmount = new Dictionary<int, string>();

                foreach (var item in db.ContentBasket)
                {
                    productAmount.Add(item.ProduktId, item.Quantity.ToString());

                }

                //nacte z databaze
                List<Produkt> tempList = new List<Produkt>();
                foreach (var item in productAmount)
                {
                    Produkt produkt = db.Produkt.Find(item.Key);
                    Produkt tempProduct = new Produkt()
                    {
                        IdProdukt = produkt.IdProdukt,
                        Color = produkt.Color,
                        Description = produkt.Description,
                        Discount = produkt.Discount,
                        Photo = produkt.Photo,
                        Price = produkt.Price,
                        ProdCat = produkt.ProdCat,
                        ProdName = produkt.ProdName,
                        Rate = produkt.Rate,
                        Size = produkt.Size,
                        Tags = produkt.Tags,
                        Vendor = produkt.Vendor
                    };


                    tempList.Add(tempProduct);

                }

                //da do views
                DefaultBasket defaultBasket = new DefaultBasket()
                {
                    BasketId = myBasket.Id,
                    ProductList = tempList,
                    Quantity = productAmount,
                    TotalPrice = int.Parse(tempList.Sum(x => x.Price).ToString())
                };

                return View(defaultBasket);
            }
            return View();
        }

        /// <summary>
        /// Vysype  kosik
        /// </summary>
        /// <returns></returns>
        public IActionResult ClearCart() 
        {
            if (db.Basket.Count() > 0)
            {
                foreach (var item in db.Basket)
                {
                    db.Basket.Remove(item);
                }
                

                foreach (var item in db.ContentBasket)
                {
                    db.ContentBasket.Remove(item);
                }

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        /// <summary>
        /// Cart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Cart() 
        {
            Models.Basket basket = GetBasket();

            return View(basket);
        }

        /// <summary>
        /// Vrati  naplneni kosik
        /// </summary>
        /// <returns></returns>
        public Models.Basket GetBasket()        
        {
            List<Produkt> prodList = new List<Produkt>();

            List<int> listId = new List<int>();
            foreach (var item in db.ContentBasket)
            {
                listId.Add(item.ProduktId);
            }

            foreach (var item in listId)
            {
                Produkt produkt = db.Produkt.Find(item);
                prodList.Add(produkt);
            }



            Models.Basket modelBasket = new Models.Basket();
            modelBasket.TotalPrice = int.Parse(prodList.Sum(x => x.Price).ToString());
            modelBasket.ProductList = prodList;
            return modelBasket;
        }

        [HttpPost]
        public IActionResult Cart(Order order)
        {
            if (ModelState.IsValid)
            {
                Data.Basket basket = db.Basket.FirstOrDefault();

                db.Order.Add(order);
                db.SaveChanges();

                basket.OrderId = order.Id;

                // save  into productOrder table
                foreach (var item in db.ContentBasket)
                {
                    var productOrder = new Data.ProductOrder
                    {
                        OrderId = order.Id,
                        ProductId = item.ProduktId,
                        Quantity = item.Quantity

                    };

                    db.ProductOrder.Add(productOrder);
                }
                


                db.Entry(basket).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();


                return RedirectToAction("CartInformation");
            }

            return View();
        }


        /// <summary>
        /// Cart information
        /// </summary>
        /// <returns></returns>
        public IActionResult CartInformation()
        {
            Models.Basket basket = GetBasket();
            return View(basket);
        }

        /// <summary>
        /// Cart shipping
        /// </summary>
        /// <returns></returns>
        public IActionResult CartShipping() 
        {
            Models.Basket basket = GetBasket();
            return View(basket);
        }

        /// <summary>
        /// Cart payment
        /// </summary>
        /// <returns></returns>
        public IActionResult CartPayment() 
        {
            Models.Basket basket = GetBasket();
            return View(basket);
        }

        /// <summary>
        /// Add articles to Cart
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity) 
        {
            var product = db.Produkt.Find(id);
            int basketId = 0;

            //  neni prazdni kosik
            if (db.Basket.Count() > 0)
            {
                var getContentOfBasket = db.ContentBasket.Where(x => x.ProduktId == id).FirstOrDefault();

                if (getContentOfBasket != null)
                {
                    getContentOfBasket.Quantity += quantity;

                    db.Entry(getContentOfBasket).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else {

                    var newItem = new Data.ContentBasket
                    {
                         ProduktId = id,
                         Quantity = quantity
                    };

                    db.ContentBasket.Add(newItem);
                    db.SaveChanges();
                }
                basketId = db.Basket.FirstOrDefault().Id;
            }
            else 
            {
                // tady je prazdni kosik
                var contentOfBasket = new  Data.ContentBasket
                {
                    ProduktId = id,
                    Quantity = quantity
                };

                var basket = new Data.Basket
                {
                    TotalPrice = int.Parse(product.Price.ToString()) * quantity
                };

                db.Basket.Add(basket);
                db.SaveChanges();

                contentOfBasket.BasketId = basket.Id;
                db.ContentBasket.Add(contentOfBasket);
                db.SaveChanges();
                basketId = basket.Id;
            }

            db.SaveChanges();


            return RedirectToAction("Index", new { idBasket = basketId});
        }
    }
}
