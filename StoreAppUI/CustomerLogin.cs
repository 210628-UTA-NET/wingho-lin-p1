using System;
using System.Collections.Generic;
using StoreAppBL;
using StoreAppModel;

namespace StoreAppUI
{
    public class CustomerLogin : IMenu
    {
        private ICustomerBL _custBL;
        private string email;
        private string phone;
        private string password;
        List<Customer> customers;
        private CustomerMenu cookie = new CustomerMenu();
        public CustomerLogin(ICustomerBL p_cust)
        {
            _custBL = p_cust;
        }
        public void Menu()
        {
            Console.WriteLine("Customer Login!");
            Console.WriteLine("Please choose a login option:");
            Console.WriteLine("[1] Password and Email");
            Console.WriteLine("[2] Password and Phone");
            Console.WriteLine("[3] Return to Start Menu.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.Write("Enter your Email:\n>> ");
                    email = Console.ReadLine().Trim();
                    Console.Write("Enter your Password:\n>> ");
                    password = Console.ReadLine().Trim();

                    customers = _custBL.GetAllCustomer();

                    foreach (Customer customer in customers)
                    {
                        if (customer.Email.Equals(email) && customer.Password == password)
                        {
                            cookie.copyCookie(customer);
                            return MenuType.CustomerMenu;
                        }
                    }
                    Console.WriteLine("Credentials do not match any current customers!");
                    return MenuType.CustomerLogin;
                case "2":
                    Console.Write("Enter your 10 digit Phone Number:\n>> ");
                    phone = Console.ReadLine();
                    Console.Write("Enter your Password:\n>> ");
                    password = Console.ReadLine();

                    customers = _custBL.GetAllCustomer();

                    foreach (Customer customer in customers)
                    {
                        if (customer.Phone == phone && customer.Password == password)
                        {
                            cookie.copyCookie(customer);
                            return MenuType.CustomerMenu;
                        }
                    }

                    Console.WriteLine("Credentials do not match any current customers!");
                    return MenuType.CustomerLogin;
                case "3":
                    return MenuType.StartMenu;
                default:
                    Console.WriteLine("Input was not recognized!");
                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                    return MenuType.CustomerLogin;
            }
        }
    }
}