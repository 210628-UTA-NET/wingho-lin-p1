using System;
using System.Collections.Generic;

#nullable disable

namespace StoreAppDL.Entities
{
    public partial class Manager
    {
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPassword { get; set; }
        public int? StoreId { get; set; }
        public string ManagerUsername { get; set; }

        public virtual StoreFront Store { get; set; }
    }
}
