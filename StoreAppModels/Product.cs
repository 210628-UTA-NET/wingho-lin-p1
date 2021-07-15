using System;
using System.Collections.Generic;

namespace StoreAppModel
{
    public class Product
    {
        private string _name;
        private double _price;
        private int _productID;
        private int _storeID;
        public Product()
        { }

        public string Name {
            get 
            {
                return _name;
            }
            set 
            {
                _name = value;
            }
        }
        public double Price {
            get 
            {
                return _price;
            }
            set 
            {
                _price = value;
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
        public int StoreID {
            get 
            {
                return _storeID;
            }
            set 
            {
                _storeID = value;
            }
        }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Product ID: {_productID}\nProduct Name: {_name}\nPrice: ${_price}";
        }
        
    }
}