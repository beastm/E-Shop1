using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class ProductOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
