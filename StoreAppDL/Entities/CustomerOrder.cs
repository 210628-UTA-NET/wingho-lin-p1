using System;
using System.Collections.Generic;

#nullable disable

namespace StoreAppDL.Entities
{
    public partial class CustomerOrder
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public string StoreAddress { get; set; }
        public decimal? OrderPrice { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
