using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class Basket
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int TotalPrice { get; set; }

        public virtual Order Order { get; set; }
    }
}
