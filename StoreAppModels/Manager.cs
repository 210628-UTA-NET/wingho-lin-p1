using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreAppModel
{
    public class Manager
    {
        [Key]
        private int _managerID;
        private string _managerName;
        private string _managerUsername;
        private StoreFront _storeFront;

        public int ManagerID 
        { 
            get
            {
                return _managerID;
            }
            set
            {
                _managerID = value;
            } 
        }
        public string ManagerName { 
            get
            {
                return _managerName;
            } 
            set
            {
                _managerName = value;
            }
        }
        public string ManagerPassword { get; set; }
        public StoreFront StoreFront
        { 
            get
            {
                return _storeFront;
            } 
            set
            {
                _storeFront = value;
            }
        }
        public string ManagerUsername 
        { 
            get
            {
                return _managerUsername;
            } 
            set
            {
                _managerUsername = value;
            }
        }

        public override string ToString()
        {
            return $"Name: {_managerName}\nUsername: {_managerUsername}\nStore ID: {_storeFront.StoreFrontID}";
        }
    }
}
