using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class ProductVM
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int StoreFrontID { get; set; }

        public ProductVM()
        { }

        public ProductVM(Product p_product)
        {
            ID = p_product.ProductID;
            Name = p_product.ProductName;
            Quantity = p_product.ProductQuantity;
            StoreFrontID = p_product.StoreFrontID;
        }
    }
}