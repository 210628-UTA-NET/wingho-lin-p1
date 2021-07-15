using System;
using System.Collections.Generic;

#nullable disable

namespace StoreAppDL.Entities
{
    public partial class StoreFront
    {
        public StoreFront()
        {
            Managers = new HashSet<Manager>();
            Products = new HashSet<Product>();
        }

        public int StoreFrontId { get; set; }
        public string StoreFrontName { get; set; }
        public string StoreFrontAddress { get; set; }

        public virtual ICollection<Manager> Managers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
