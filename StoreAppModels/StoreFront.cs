using System;
using System.Collections.Generic;

namespace StoreAppModel
{
    public class StoreFront
    {
        private string _storeFrontName;
        private string _storeFrontAddress;
        private List<LineItem> _inventory;
        private List<Order> _orders;
        private int _storeFrontID;

        public int StoreFrontID 
        { 
            get
            {
                return _storeFrontID;
            } 
            set
            {
                _storeFrontID = value;
            }
        }
        public string Name { 
            get
            {
                return _storeFrontName;
            }
            set 
            {
                _storeFrontName = value;
            }
        }
        public string Address { 
            get
            {
                return _storeFrontAddress;
            }
            set
            {
                _storeFrontAddress = value;
            }
        }

        private void AddOrder(Order p_order)
        {
            _orders.Add(p_order);
        }

        public override string ToString()
        {
            return $"Name: {_storeFrontName}\nAddress: {_storeFrontAddress}\nStore ID: {_storeFrontID}";
        }
    }
}
