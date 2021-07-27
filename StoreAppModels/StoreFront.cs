using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreAppModel
{
    public class StoreFront
    {
        [Key]
        private int _storeFrontID;
        private string _storeFrontName;
        private string _storeFrontAddress;
        private List<LineItem> _inventory;
        private List<Order> _storeFrontOrders;
        
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
        public string StoreFrontName { 
            get
            {
                return _storeFrontName;
            }
            set 
            {
                _storeFrontName = value;
            }
        }
        public string StoreFrontAddress { 
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
            _storeFrontOrders.Add(p_order);
        }
        public List<Order> StoreFrontOrders { 
            get
            {
                return _storeFrontOrders;
            } 
            set
            {
                _storeFrontOrders = value;
            }
        }

        public override string ToString()
        {
            return $"Name: {_storeFrontName}\nAddress: {_storeFrontAddress}\nStore ID: {_storeFrontID}";
        }
    }
}
