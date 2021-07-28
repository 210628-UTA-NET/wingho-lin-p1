using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class OrderVM
    {
        public StoreAppModel.Order Order { get; set; }
     
        
        public OrderVM() 
        { }

        public OrderVM(StoreAppModel.Order p_order)
        {
            Order = p_order;
        }
    }
}