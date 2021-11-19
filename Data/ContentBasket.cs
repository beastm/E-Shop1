using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class ContentBasket
    {
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProduktId { get; set; }
        public int Quantity { get; set; }
    }
}
