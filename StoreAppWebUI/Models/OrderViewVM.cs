using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class OrderViewVM
    {
        public List<StoreAppModel.Order> Orders { get; set; }
     
        
        public OrderViewVM() 
        { }

        public OrderViewVM(List<StoreAppModel.Order> p_orders)
        {
            Orders = p_orders;
        }
    }
}