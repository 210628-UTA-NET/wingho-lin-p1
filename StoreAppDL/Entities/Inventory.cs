using System;
using System.Collections.Generic;

#nullable disable

namespace StoreAppDL.Entities
{
    public partial class Inventory
    {
        public int? ProductId { get; set; }
        public int? InventoryQuantity { get; set; }

        public virtual Product Product { get; set; }
    }
}
