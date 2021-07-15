using System;
using System.Collections.Generic;

#nullable disable

namespace StoreAppDL.Entities
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? StoreId { get; set; }
        public decimal? ProductPrice { get; set; }

        public virtual StoreFront Store { get; set; }
    }
}
