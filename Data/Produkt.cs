using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class Produkt
    {
        public Produkt()
        {
            ProdCat = new HashSet<ProdCat>();
        }

        public int IdProdukt { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
        public int Rate { get; set; }
        public string Vendor { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Tags { get; set; }
        public int Discount { get; set; }
        public string ProdName { get; set; }

        public virtual ICollection<ProdCat> ProdCat { get; set; }
    }
}
