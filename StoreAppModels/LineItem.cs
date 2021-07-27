using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreAppModel
{
    public class LineItem
    {
        [Key]
        private int _lineItemID;
        private int _lineItemQuantity;
        private Product _product;
        private Order _order;
        public LineItem()
        { }

        public int LineItemID {
            get
            {
                return _lineItemID;
            }
            set
            {
                _lineItemID = value;
            }
        }
        public int LineItemQuantity {
            get 
            {
                return _lineItemQuantity;
            }
            set 
            {
                _lineItemQuantity = value;
            }
        }
        public Product Product {
            get 
            {
                return _product;
            }
            set 
            {
                _product = value;
            }
        }
        public Order Order {
            get
            {
                return _order;
            }
            set
            {
                _order = value;
            }
        }

        public override string ToString()
        {
            return $"Product ID: {_product.ProductID}\nProduct Name: {_product.ProductName}\nQuantity: {_lineItemQuantity}";
        }
    }
}