using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StoreAppModel
{
    public class Customer
    {
        private string _name;
        private string _address;
        private string _email;
        private string _phone;
        private string _password;

        private List<LineItem> _orders;

        public Customer()
        { }

        public Customer(string p_name)
        {
            _name = p_name;
        }
        public int ID { get; set; }
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
        public string Address {
            get
            {
                return _address;
            } 

            set
            {
                _address = value;
            }

        }
        public string Email {
            get
            {
                return _email;
            } 

            set
            {
                _email = value;
            }
        }
        public string Phone {
            get
            {
                return _phone;
            } 

            set
            {
                _phone = value;
            }
        }

        public string Password{ 
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            } 
        }
        public List<LineItem> Orders {
            get
            {
                return _orders;
            }
            set
            {
                _orders = value;
            }
        }

        public void AddOrder(LineItem p_order)
        {
            _orders.Add(p_order);
        }
        
        public override string ToString()
        {
            return $"Name: {_name}\nAddress: {_address}\nEmail: {_email}\nPhone: {_phone}";
        }
    }
}
