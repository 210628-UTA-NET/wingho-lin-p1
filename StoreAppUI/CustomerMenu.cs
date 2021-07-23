using System;
using System.Threading;
using StoreAppModel;
using System.Collections.Generic;
using StoreAppBL;

namespace StoreAppUI
{
    public class CustomerMenu : IMenu
    {
        public static Customer customer = new Customer();
        private List<StoreFront> StoreList;
        private ICustomerBL _custBL;
        private int StoreID;
        private PlaceOrderMenu orderMenu = new PlaceOrderMenu();
        public void copyCookie(Customer cookie)
        {
            customer.ID = cookie.ID;
            customer.Name = cookie.Name;
            customer.Email = cookie.Email;
            customer.Phone = cookie.Phone;
        }
        public CustomerMenu(ICustomerBL p_cust)
        {
            _custBL = p_cust;
        }
        public CustomerMenu()
        { }

        private List<int> GetAvailableStoreIDs(List<StoreFront> StoresList)
        {
            List<int> StoreIDs = new List<int>();

            foreach (StoreFront store in StoresList)
            {
                StoreIDs.Add(store.StoreFrontID);
            }

            return StoreIDs;
        }
        public void Menu()
        {
            Console.WriteLine("Welcome to Customer Menu!");
            Console.WriteLine("Please input an integer to select an option:");
            Console.WriteLine("[1] Place an Order.");
            Console.WriteLine("[2] View Store Inventory.");
            Console.WriteLine("[3] View Order history.");
            Console.WriteLine("[4] Sign out.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();
            List<int> StoreIDs = new List<int>();

            switch (userChoice)
            {
                case "1":
                    StoreList = _custBL.GetAllStoreFronts();

                    if (StoreList.Count > 0)
                    {
                        Console.WriteLine("List of Stores:");
                        Console.WriteLine(ASCII.ListDelimiter);
                        foreach (StoreFront store in StoreList)
                        {
                            Console.WriteLine(store.ToString());
                            Console.WriteLine(ASCII.ListDelimiter);
                        }
                        
                        StoreIDs = GetAvailableStoreIDs(StoreList);
                        Console.Write("Choose a store and enter its Store ID:\n>> ");
                        StoreID = int.Parse(Console.ReadLine());
                        if (StoreIDs.Contains(StoreID))
                        {
                            orderMenu.copyCookie(customer, StoreID);
                        } else
                        {
                            Console.WriteLine("Please enter the integer number of a store's ID!\nPress Enter to continue...");
                            Console.ReadLine();
                        }
                        
                    }
                    
                    return MenuType.PlaceOrderMenu;
                case "2":
                    // ask for store id first
                    StoreList = _custBL.GetAllStoreFronts();
                    
                    Console.WriteLine("List of Stores:");
                    Console.WriteLine(ASCII.ListDelimiter);
                    foreach (StoreFront store in StoreList)
                    {
                        Console.WriteLine(store.ToString());
                        Console.WriteLine(ASCII.ListDelimiter);
                    }
                    
                    Console.Write("\nChoose a store and enter its Store ID:\n>> ");
                    StoreID = int.Parse(Console.ReadLine());

                    List<Product> Inventory = _custBL.GetStoreProducts(StoreID);
                    Console.WriteLine("List of Store's inventory:\n"+ASCII.ListDelimiter);
                    foreach (Product item in Inventory)
                    {
                        Console.WriteLine(item.ToString());
                        Console.WriteLine(ASCII.ListDelimiter);
                    }

                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return MenuType.CustomerMenu;
                case "3":
                    List<Order> OrderHistory = _custBL.GetCustomerOrders(customer.ID);
                    if (OrderHistory.Count > 0)
                    {
                        Console.WriteLine("Order history:");
                        Console.WriteLine(ASCII.ListDelimiter);
                        foreach (Order order in OrderHistory)
                        {
                            Console.WriteLine(order.ToString());
                            Console.WriteLine(ASCII.ListDelimiter);
                        }
                    } else
                    {
                        Console.WriteLine("You have not made any orders yet!");
                    }
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return MenuType.CustomerMenu;
                case "4":
                    return MenuType.StartMenu;
                default:
                    Console.WriteLine("Input was not correct");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.CustomerMenu;
            }
        }
    }
}