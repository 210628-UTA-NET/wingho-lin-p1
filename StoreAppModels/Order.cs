using System;
using System.Collections.Generic;

namespace StoreAppModel
{
    public class Order
    {
        private List<LineItem> _orderLineItems;
        private string _location;
        private double _totalPrice;
        public Order()
        { 
            _orderLineItems = new List<LineItem>();
        }

        public List<LineItem> OrderLineItems {
            get 
            {
                return _orderLineItems;
            }
            set 
            {
                _orderLineItems = value;
            }
        }
        public string Location {
            get 
            {
                return _location;
            }
            set 
            {
                _location = value;
            }
        }
        public double TotalPrice {
            get 
            {
                return _totalPrice;
            }
            set 
            {
                _totalPrice = value;
            }
        }

        public int CustomerID { get; set; }
        public int OrderID { get; set; }

        public override string ToString()
        {
            return $"Order ID: {OrderID}\nTotal: ${_totalPrice}\nLocation: {_location}";
        }

        public void AddOrderLineItem(LineItem item)
        {
            _orderLineItems.Add(item);
        }
        
    }
}