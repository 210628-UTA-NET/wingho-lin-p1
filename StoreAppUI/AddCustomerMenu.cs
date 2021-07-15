using System;
using System.Threading;
using StoreAppBL;
using StoreAppModel;
using System.Collections.Generic;

namespace StoreAppUI
{
    public class AddCustomerMenu : IMenu
    {
        // global customer to store fields entered while in addcustomer menu
        private static Customer _newcust = new Customer();
        private ICustomerBL _custBL;
        private string input = "";
        public AddCustomerMenu(ICustomerBL p_custBL)
        {
            _custBL = p_custBL;
        }
        private List<Customer> customers;
        public void Menu()
        {
            Console.WriteLine(ASCII.AddCustomer);
            Console.WriteLine("[1] Name - " + _newcust.Name);
            Console.WriteLine("[2] Address - " + _newcust.Address);
            Console.WriteLine("[3] Email - " + _newcust.Email);
            Console.WriteLine("[4] Phone - " + _newcust.Phone);
            Console.WriteLine("[5] Password - " + _newcust.Password);
            Console.WriteLine("[6] Add Customer.");
            Console.WriteLine("[7] Return to Start Menu.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.Write("Enter Customer Name:\n>> ");
                    _newcust.Name = Console.ReadLine();
                    return MenuType.AddCustomerMenu;
                case "2":
                    Console.Write("Enter Customer Address:\n>> ");
                    _newcust.Address = Console.ReadLine();
                    return MenuType.AddCustomerMenu;
                case "3":
                    Console.Write("Enter Customer Email:\n>> ");
                    _newcust.Email = Console.ReadLine();
                    return MenuType.AddCustomerMenu;
                case "4":
                    Console.Write("Enter a 10-digit Phone Number: (ex: 1234567890)\n>> ");
                    input = Console.ReadLine();
                    if (input.Length != 10)
                    {
                        Console.WriteLine("Phone number has to have only 10 digits!\nPress enter to continue.");
                        Console.ReadLine();
                        return MenuType.AddCustomerMenu;
                    }
                    _newcust.Phone = input;
                    return MenuType.AddCustomerMenu;
                case "5":
                    Console.Write("Enter a password (NOTE! Do not use a real password. Database security is relatively weak at the moment!):\n>> ");
                    _newcust.Password = Console.ReadLine();
                    return MenuType.AddCustomerMenu;
                case "6":
                    // email or phone has to have a value, and password has to have a value
                    if (_newcust.Email != null || _newcust.Phone != null)
                    {
                        if (_newcust.Password != null)
                        {
                            // find an appropriate ID
                            customers = _custBL.GetAllCustomer();
                            _newcust.ID = customers.Count + 1;

                            _custBL.AddCustomer(_newcust);
                            Console.WriteLine("Customer added successfully!\nRedirecting to Start Menu...");
                            System.Threading.Thread.Sleep(1000);  // Delay slightly to notify user of successful add
                            _newcust = null;
                            return MenuType.StartMenu;
                        }
                        Console.WriteLine("You have to enter a password!");
                        Console.WriteLine("\nPress Enter to continue.");
                        Console.ReadLine();
                        return MenuType.AddCustomerMenu;
                        
                    } 
                    Console.WriteLine("You have to enter either an Email or a Phone number!");
                    Console.WriteLine("\nPress Enter to continue.");
                    Console.ReadLine();
                    return MenuType.AddCustomerMenu;
                case "7":
                    return MenuType.StartMenu;
                default:
                    Console.WriteLine("Input was not correct");
                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                    return MenuType.AddCustomerMenu;
            }
        }
    }
}