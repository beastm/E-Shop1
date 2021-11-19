using Eshop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public int? ProduktId { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public List<Produkt> ProductList { get; set; }
    }
}
