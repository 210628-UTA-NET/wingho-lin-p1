using System;
using System.Collections.Generic;

namespace StoreAppModel
{
    public class Manager
    {
        private string _name;
        private string _username;
        private int _storeID;

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
        public string Password { get; set; }
        public int StoreID 
        { 
            get
            {
                return _storeID;
            } 
            set
            {
                _storeID = value;
            }
        }
        public string Username 
        { 
            get
            {
                return _username;
            } 
            set
            {
                _username = value;
            }
        }

        public override string ToString()
        {
            return $"Name: {_name}\nUsername: {_username}\nStore ID: {_storeID}";
        }
    }
}
