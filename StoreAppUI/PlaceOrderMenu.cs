using System;
using System.Threading;
using StoreAppModel;
using System.Collections.Generic;
using StoreAppBL;

namespace StoreAppUI
{
    public class PlaceOrderMenu : IMenu
    {
        private static Customer customer = new Customer();
        private ICustomerBL _custBL;
        public static int StoreID;
        private Order CurrentOrder = new Order();
        private static LineItem CurrentItem = new LineItem();
        private static double PriceTotal = 0;
        string input;
        private List<Product> StoreProducts = new List<Product>();
        string location = "";
        public void copyCookie(Customer p_customer, int p_storeID)
        {
            customer.ID = p_customer.ID;
            StoreID = p_storeID;
            
        }
        public PlaceOrderMenu(ICustomerBL p_cust)
        {
            _custBL = p_cust;
        }
        public PlaceOrderMenu()
        { }
        public void Menu()
        {
            StoreProducts = _custBL.GetStoreProducts(StoreID);
            Console.WriteLine("Available Store products:\n"+ASCII.ListDelimiter);
            foreach (Product item in StoreProducts)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine(ASCII.ListDelimiter);
            }

            Console.WriteLine("Welcome to the Order Placing Menu!");
            Console.WriteLine("Please input an integer to select an option:");
            Console.WriteLine($"Total: ${PriceTotal}");
            Console.WriteLine($"[1] Product ID - {CurrentItem.ProductID}");
            Console.WriteLine($"[2] Product Quantity - {CurrentItem.Quantity}");
            Console.WriteLine("[3] Add to cart. ");
            Console.WriteLine("[4] Place Order.");
            Console.WriteLine("[5] Return to Customer Menu.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    Console.Write("Enter Product ID:\n>> ");
                    input = Console.ReadLine();
                    CurrentItem.ProductID = int.Parse(input);
                    
                    return MenuType.PlaceOrderMenu;
                case "2":
                    Console.Write("Enter Product Quantity:\n>> ");
                    input = Console.ReadLine();
                    CurrentItem.Quantity = int.Parse(input);

                    return MenuType.PlaceOrderMenu;
                case "3":
                    CurrentOrder.AddOrderLineItem(CurrentItem);

                    foreach (Product product in StoreProducts)
                    {
                        if (CurrentItem.ProductID == product.ProductID)
                        {
                            PriceTotal += product.Price * CurrentItem.Quantity;
                            break;
                        }
                    }
                    Console.WriteLine("Added to cart!\nPress Enter to continue.");
                    Console.ReadLine();
                    return MenuType.PlaceOrderMenu;
                case "4":
                    location = _custBL.GetStoreLocation(StoreID);
                    CurrentOrder.Location = location;
                    if (_custBL.PlaceOrder(CurrentOrder, PriceTotal,customer.ID))
                    {
                        Console.WriteLine("Order was placed succesfully!\nPress Enter to be returned to Customer Menu");
                        Console.ReadLine();
                        PriceTotal = 0;
                        return MenuType.CustomerMenu;
                    } else
                    {
                        Console.WriteLine("Order was placed succesfully!\nPress Enter to continue.");
                        Console.ReadLine();
                    }
                    return MenuType.PlaceOrderMenu;
                case "5":
                    return MenuType.CustomerMenu;
                default:
                    Console.WriteLine("Input was not correct");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.CustomerMenu;
            }
        }
    }
}