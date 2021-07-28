using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class StoreVM
    {
        [Required]
        public int ID { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        public List<Product> Inventory { get; set; }
        public List<Order> Orders { get; set; }

        public StoreVM()
        { }

        public StoreVM(int p_StoreID)
        {
            ID = p_StoreID;
        }

        public StoreVM(StoreFront p_store)
        {
            ID = p_store.StoreFrontID;
            Name = p_store.StoreFrontName;
            Address = p_store.StoreFrontAddress;
            Orders = p_store.StoreFrontOrders;
            Inventory = p_store.Inventory;
        }
    }
}