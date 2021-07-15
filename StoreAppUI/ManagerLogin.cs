using System;
using System.Collections.Generic;
using StoreAppBL;
using StoreAppModel;

namespace StoreAppUI
{
    public class ManagerLogin : IMenu
    {
        private ICustomerBL _custBL;
        private string username;
        private string password;
        List<Manager> managers;
        private ManagerMenu cookie = new ManagerMenu();
        public ManagerLogin(ICustomerBL p_man)
        {
            _custBL = p_man;
        }
        public void Menu()
        {
            Console.WriteLine("Manager Login!");
            Console.WriteLine("Please choose a login option:");
            Console.WriteLine("[1] Username and Password.");
            Console.WriteLine("[2] Return to Start Menu.");
            Console.Write(">> ");
        }

        public MenuType YourChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    Console.Write("Enter your Username:\n>> ");
                    username = Console.ReadLine().Trim();
                    Console.Write("Enter your Password:\n>> ");
                    password = Console.ReadLine().Trim();

                    managers = _custBL.GetAllManagers();

                    foreach (Manager manager in managers)
                    {
                        if (manager.Username == username && manager.Password == password)
                        {
                            cookie.copyCookie(manager);
                            return MenuType.ManagerMenu;
                        }

                        //debugging
                        Console.WriteLine(manager.ToString());
                        if (manager.Username != username)
                        {
                            Console.WriteLine($"username doesn't match:\n{manager.Username}\n{username}");
                        } else if (manager.Password != password)
                        {
                            Console.WriteLine($"password doesn't match:\n{manager.Password}\n{password}");
                        }
                    }
                    Console.WriteLine("Credentials do not match any current managers!");
                    return MenuType.ManagerLogin;
                
                case "2":
                    return MenuType.StartMenu;
                default:
                    Console.WriteLine("Input was not recognized!");
                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                    return MenuType.ManagerLogin;
            }
        }
    }
}