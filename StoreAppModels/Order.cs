using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreAppModel
{
    public class Order
    {
        [Key]
        private int _orderID;
        private List<LineItem> _orderLineItems;
        private StoreFront _storeFront;
        private double _orderPrice;
        private Customer _customer;

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
        public double OrderPrice {
            get 
            {
                return _orderPrice;
            }
            set 
            {
                _orderPrice = value;
            }
        }

        public Customer Customer { 
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
            }
        }
        public int OrderID { get; set; }

        public DateTime DatePlaced { get; set; }

        public override string ToString()
        {
            return $"Order ID: {_orderID}\nTotal: ${_orderPrice}\nLocation: {_storeFront.StoreFrontAddress}";
        }

        public void AddOrderLineItem(LineItem item)
        {
            _orderLineItems.Add(item);
        }
        
    }
}