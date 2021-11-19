using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class Category
    {
        public Category()
        {
            ProdCat = new HashSet<ProdCat>();
        }

        public string CatName { get; set; }
        public int Id { get; set; }

        public virtual ICollection<ProdCat> ProdCat { get; set; }
    }
}
