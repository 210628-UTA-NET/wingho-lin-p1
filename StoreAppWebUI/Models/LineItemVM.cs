using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class LineItemVM
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        public LineItemVM()
        { }

        public LineItemVM(LineItem p_item)
        {
            ID = p_item.LineItemID;
            OrderID = p_item.Order.OrderID;
            Quantity = p_item.LineItemQuantity;
            Product = new Product()
            {
                ProductID = p_item.Product.ProductID,
                ProductName = p_item.Product.ProductName,
                ProductPrice = p_item.Product.ProductPrice
            };
        }
    }
}