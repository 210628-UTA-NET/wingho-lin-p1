using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class OrderVM
    {
        public int ID { get; set; }

        public double Price { get; set; }

        public int StoreFrontID { get; set; }
        
        public DateTime DatePlaced { get; set; }
        
        public OrderVM() 
        { }

        public OrderVM(StoreAppModel.Order p_order)
        {
            ID = p_order.OrderID;
            Price = p_order.OrderPrice;
            StoreFrontID = p_order.StoreFrontID;
            DatePlaced = p_order.DatePlaced;
        }
    }
}