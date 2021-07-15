using System;
using System.Collections.Generic;
using StoreAppBL;
using StoreAppModel;

namespace StoreAppUI
{
    public class SearchCustomerMenu : IMenu
    {
        // global customer to store fields entered while in SearchCustomer menu
        private static Customer _newcust = new Customer();
        private ICustomerBL _custBL;
        private static bool emailFlag = false;
        private static bool phoneFlag = false;
        public SearchCustomerMenu(ICustomerBL p_cust)
        {
            _custBL = p_cust;
        }
        public void Menu()
        {
            Console.WriteLine(ASCII.SearchCustomer);
            Console.WriteLine("Fill out the fields that you want to search by:");
            Console.WriteLine("[1] Email - " + _newcust.Email);
            Console.WriteLine("[2] Phone - " + _newcust.Phone);
            Console.WriteLine("[3] Search for Customer.");
            Console.WriteLine("[4] Return to Manager Menu.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.WriteLine("Enter Customer Email:");
                    Console.Write(">> ");
                    _newcust.Email = Console.ReadLine().Trim();
                    return MenuType.SearchCustomerMenu;
                case "2":
                    Console.WriteLine("Enter Customer Phone Number:");
                    _newcust.Phone = Console.ReadLine().Trim();
                    Console.Write(">> ");
                    return MenuType.SearchCustomerMenu;
                case "3":
                    // make printed result easier to see
                    Console.WriteLine("\n" + ASCII.ListDelimiter);

                    // check and set search flags
                    if (_newcust.Email != "" && _newcust.Email != null)
                    {
                        emailFlag = true;
                    } else if (_newcust.Email == "")
                    {
                        emailFlag = false;
                    }
                    if (_newcust.Phone != "" && _newcust.Phone != null)
                    {
                        phoneFlag = true;
                    } else if (_newcust.Phone == "")
                    {
                        phoneFlag = false;
                    }

                    List<Customer> customers = _custBL.GetAllCustomer();

                    if (emailFlag && phoneFlag)
                    {
                        foreach (Customer customer in customers)
                        {
                            if (customer.Email == _newcust.Email && customer.Phone == _newcust.Phone)
                            {
                                Console.WriteLine("Customer found!\n" + customer.ToString());
                                Console.WriteLine("\nPress enter to continue");
                                Console.ReadLine();
                                return MenuType.SearchCustomerMenu;
                            }
                        }
                    } else if (emailFlag)
                    {
                        foreach (Customer customer in customers)
                        {
                            if (customer.Email.Equals(_newcust.Email))
                            {
                                Console.WriteLine("Customer found!\n" + customer.ToString());
                                Console.WriteLine("\nPress enter to continue");
                                Console.ReadLine();
                                return MenuType.SearchCustomerMenu;
                            }
                        }
                    } else if (phoneFlag)
                    {
                        foreach (Customer customer in customers)
                        {
                            if (customer.Phone == _newcust.Phone)
                            {
                                Console.WriteLine("Customer found!\n" + customer.ToString());
                                Console.WriteLine("\nPress enter to continue");
                                Console.ReadLine();
                                return MenuType.SearchCustomerMenu;
                            }
                        }
                    }
                    Console.WriteLine($"There were no matches for email: {_newcust.Email} with the phone number: {_newcust.Phone}");
                    Console.WriteLine("\nPress enter to continue");
                    Console.ReadLine();
                    return MenuType.SearchCustomerMenu;
                    //return MenuType.ShowCustomerMenu;
                case "4":
                    return MenuType.ManagerMenu;
                default:
                    Console.WriteLine("Input was not recognized!");
                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                    return MenuType.SearchCustomerMenu;
            }
        }
    }
}