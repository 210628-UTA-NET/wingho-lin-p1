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

            switch (userChoice)
            {
                case "1":
                    StoreList = _custBL.GetAllStoreFronts();

                    Console.WriteLine("List of Stores:");
                    Console.WriteLine(ASCII.ListDelimiter);
                    foreach (StoreFront store in StoreList)
                    {
                        Console.WriteLine(store.ToString());
                        Console.WriteLine(ASCII.ListDelimiter);
                    }
                    
                    Console.Write("Choose a store and enter its Store ID:\n>> ");
                    StoreID = int.Parse(Console.ReadLine());
                    orderMenu.copyCookie(customer, StoreID);
                    return MenuType.PlaceOrderMenu;
                case "2":
                    // select from Inventory, Products, and LineItems tables in the database to compile an inventory
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

                    List<LineItem> Inventory = _custBL.GetStoreInventory(StoreID);
                    List<Product> Products = _custBL.GetStoreProducts(StoreID);
                    Console.WriteLine("List of Store's inventory:\n"+ASCII.ListDelimiter);
                    foreach (LineItem item in Inventory)
                    {
                        Console.WriteLine(item.ToString());

                        // find and print out price since LineItem type doesn't hold price
                        foreach (Product product in Products)
                        {
                            if (item.ProductID == product.ProductID)
                            {
                                Console.WriteLine($"Price: ${product.Price}");
                            }
                        }
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