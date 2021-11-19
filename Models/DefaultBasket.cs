using Eshop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class DefaultBasket
    {
        public int BasketId { get; set; }
        public int? ProduktId { get; set; }
        public int? OrderId { get; set; }
        public Dictionary<int,string> Quantity { get; set; }
        public int TotalPrice { get; set; }

        public List<Produkt> ProductList { get; set; }
    }
}
