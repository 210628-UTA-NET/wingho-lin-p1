using System;
using System.Collections.Generic;
using StoreAppBL;
using StoreAppModel;

namespace StoreAppUI
{
    public class ShowCustomerMenu : IMenu
    {
        private ICustomerBL _custBL;
        public ShowCustomerMenu(ICustomerBL p_cust)
        {
            _custBL = p_cust;
        }
        public void Menu()
        {
            Console.WriteLine("List of Customers:");

            List<Customer> customers = _custBL.GetAllCustomer();

            Console.WriteLine(ASCII.ListDelimiter);
            
            foreach (Customer cust in customers)
            {
                Console.WriteLine(cust);
                Console.WriteLine(ASCII.ListDelimiter);
            }

            Console.WriteLine("");

            Console.WriteLine("[1] Return to Customer Search Menu");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    return MenuType.SearchCustomerMenu;
                default:
                    Console.WriteLine("Input was not correct");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.SearchCustomerMenu;
            }
        }
    }
}