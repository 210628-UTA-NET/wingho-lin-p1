using System;
using System.Threading;
using StoreAppModel;
using StoreAppBL;
using System.Collections.Generic;

namespace StoreAppUI
{
    public class ManagerMenu : IMenu
    {
        public static Manager manager = new Manager();
        private ICustomerBL _custBL;
        private int Quantity = 0;
        private int ProductID;
        List<LineItem> Inventory = new List<LineItem>();
        List<Product> Products = new List<Product>();
        bool ReplenishSuccess = false;
        public void copyCookie(Manager cookie)
        {
            manager.ID = cookie.ID;
            manager.Name = cookie.Name;
            manager.Username = cookie.Username;
            manager.StoreID = cookie.StoreID;
        }
        public ManagerMenu(ICustomerBL p_man)
        {
            _custBL = p_man;
        }
        public ManagerMenu()
        { }

        public void Menu()
        {
            Console.WriteLine("Welcome to the Manager Menu!");
            Console.WriteLine("Please input an integer to select an option:");
            Console.WriteLine("[1] Restock inventory.");
            Console.WriteLine("[2] View Store Inventory.");
            Console.WriteLine("[3] View Store Order history.");
            Console.WriteLine("[4] Search for customer.");
            Console.WriteLine("[5] Sign out.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    //Console.WriteLine("restock");
                    Inventory = _custBL.GetStoreInventory(manager.StoreID);
                    Products = _custBL.GetStoreProducts(manager.StoreID);
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

                    Console.Write("Choose a product ID and an amount to restock/add.\nProduct ID: ");

                    ProductID = int.Parse(Console.ReadLine());
                    Console.Write("Quantity: ");
                    Quantity = int.Parse(Console.ReadLine());
                    ReplenishSuccess = _custBL.ReplenishInventory(ProductID, Quantity);
                    if (ReplenishSuccess)
                    {
                        Console.WriteLine("Replenished successfully!\nPress Enter to continue.");
                        Console.ReadLine();
                    } else
                    {
                        Console.WriteLine("Replenish failed!\nPress Enter to continue and try again later.");
                        Console.ReadLine();
                    }
                    return MenuType.ManagerMenu;
                case "2":
                    Inventory = _custBL.GetStoreInventory(manager.StoreID);
                    Products = _custBL.GetStoreProducts(manager.StoreID);
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
                    return MenuType.ManagerMenu;
                case "3":
                    List<Order> OrderHistory = _custBL.GetStoreFrontOrders(manager.StoreID);
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
                        Console.WriteLine("There are currently no past orders for your store!");
                    }
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return MenuType.ManagerMenu;
                case "4":
                    return MenuType.SearchCustomerMenu;
                case "5":
                    return MenuType.StartMenu;
                default:
                    Console.WriteLine("Input was not correct");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.ManagerMenu;
            }
        }
    }
}