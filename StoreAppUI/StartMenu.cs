using System;
using StoreAppBL;
using StoreAppModel;

namespace StoreAppUI
{
    public class StartMenu : IMenu
    {
        private ICustomerBL _custBL;
        
        public void Menu()
        {
            Console.WriteLine("Welcome to the Start Menu!");
            Console.WriteLine("Please input an integer to select an option:");
            Console.WriteLine("[1] Create new Customer.");
            Console.WriteLine("[2] Login to existing Customer.");
            Console.WriteLine("[3] Login as Manager.");
            Console.WriteLine("[4] Exit program.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    return MenuType.AddCustomerMenu;
                case "2":
                    return MenuType.CustomerLogin;
                case "3":
                    return MenuType.ManagerLogin;
                case "4":
                    return MenuType.Exit;
                default:
                    Console.WriteLine("Input was not correct");
                    Console.WriteLine("Press Enter to continue");
                    Console.ReadLine();
                    return MenuType.StartMenu;
            }
        }
    }
}