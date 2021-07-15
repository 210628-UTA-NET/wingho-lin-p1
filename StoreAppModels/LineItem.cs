using System;
using System.Collections.Generic;

namespace StoreAppModel
{
    public class LineItem
    {
        private string _productName;
        private int _quantity;
        private int _productID;
        public LineItem()
        { }

        public string ProductName {
            get 
            {
                return _productName;
            }
            set 
            {
                _productName = value;
            }
        }
        public int Quantity {
            get 
            {
                return _quantity;
            }
            set 
            {
                _quantity = value;
            }
        }
        public int ProductID {
            get 
            {
                return _productID;
            }
            set 
            {
                _productID = value;
            }
        }

        public override string ToString()
        {
            return $"Product ID: {_productID}\nProduct Name: {_productName}\nQuantity: {Quantity}";
        }
    }
}