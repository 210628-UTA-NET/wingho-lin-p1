using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class CustomerViewVM
    {
        public List<StoreAppModel.Customer> Customers { get; set; }
     
        
        public CustomerViewVM() 
        { }

        public CustomerViewVM(List<StoreAppModel.Customer> p_customers)
        {
            Customers = p_customers;
        }
    }
}