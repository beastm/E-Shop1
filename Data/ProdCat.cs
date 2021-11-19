using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class ProdCat
    {
        public int CatId { get; set; }
        public int ProduktId { get; set; }

        public virtual Category Cat { get; set; }
        public virtual Produkt Produkt { get; set; }
    }
}
