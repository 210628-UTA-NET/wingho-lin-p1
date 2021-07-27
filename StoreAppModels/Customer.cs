using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace StoreAppModel
{
    public class Customer
    {
        [Key]
        private int _customerID;
        private string _customerName;
        private string _customerAddress;
        private string _customerEmail;
        private string _customerPhone;
        private string _customerPassword;

        private List<Order> _customerOrders;

        public Customer()
        { }

        public Customer(string p_name)
        {
            _customerName = p_name;
        }
        public int CustomerID { 
            get
            {
                return _customerID;
            } 
            set
            {
                _customerID = value;
            } 
        }
        public string CustomerName { 
            get
            {
                return _customerName;
            } 

            set
            {
                _customerName = value;
            }
        }
        public string CustomerAddress {
            get
            {
                return _customerAddress;
            } 

            set
            {
                _customerAddress = value;
            }

        }
        public string CustomerEmail {
            get
            {
                return _customerEmail;
            } 

            set
            {
                _customerEmail = value;
            }
        }
        public string CustomerPhone {
            get
            {
                return _customerPhone;
            } 

            set
            {
                _customerPhone = value;
            }
        }

        public string CustomerPassword{ 
            get
            {
                return _customerPassword;
            }

            set
            {
                _customerPassword = value;
            } 
        }
        public List<Order> CustomerOrders {
            get
            {
                return _customerOrders;
            }
            set
            {
                _customerOrders = value;
            }
        }

        public void AddOrder(Order p_order)
        {
            _customerOrders.Add(p_order);
        }
        
        public override string ToString()
        {
            return $"Name: {_customerName}\nAddress: {_customerAddress}\nEmail: {_customerEmail}\nPhone: {_customerPhone}";
        }
    }
}
