using System;
using System.Collections.Generic;

#nullable disable

namespace StoreAppDL.Entities
{
    public partial class LineItem
    {
        public int? ProductId { get; set; }
        public int? LineItemQuantity { get; set; }
        public int? OrderId { get; set; }

        public virtual CustomerOrder Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
