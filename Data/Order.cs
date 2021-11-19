using System;
using System.Collections.Generic;

namespace Eshop.Data
{
    public partial class Order
    {
        public Order()
        {
            Basket = new HashSet<Basket>();
        }

        public int Id { get; set; }
        public string ContactInfo { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Apartment { get; set; }
        public string City { get; set; }
        public string Postalcode { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Basket> Basket { get; set; }
    }
}
