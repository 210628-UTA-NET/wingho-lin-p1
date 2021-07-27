using System;
using System.Collections.Generic;

namespace StoreAppModel
{
    public class Product
    {
        private string _productName;
        private double _productPrice;
        private int _productID;
        private StoreFront _storeFront;
        private int _productQuantity;
        public Product()
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
        public double ProductPrice {
            get 
            {
                return _productPrice;
            }
            set 
            {
                _productPrice = value;
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
        public StoreFront StoreFront {
            get 
            {
                return _storeFront;
            }
            set 
            {
                _storeFront = value;
            }
        }
        public int ProductQuantity {
            get
            {
                return _productQuantity;
            }
            set
            {
                _productQuantity = value;
            }
        }

        public override string ToString()
        {
            return $"Product ID: {_productID}\nProduct Name: {_productName}\nPrice: ${_productPrice}\nQuantity: {_productQuantity}";
        }
        
    }
}