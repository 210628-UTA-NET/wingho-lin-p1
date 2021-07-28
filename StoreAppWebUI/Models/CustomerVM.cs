using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using StoreAppModel;

namespace StoreAppWebUI.Models
{
    public class CustomerVM
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public List<Order> Orders { get; set; }

        public CustomerVM()
        { }

        public CustomerVM(Customer p_customer)
        {
            ID = p_customer.CustomerID;
            Name = p_customer.CustomerName;
            Address = p_customer.CustomerAddress;
            Email = p_customer.CustomerEmail;
            PhoneNumber = p_customer.CustomerPhone;
            Orders = p_customer.CustomerOrders;
        }
    }
}