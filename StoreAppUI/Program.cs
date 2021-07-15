using System;
using StoreAppBL;
using StoreAppDL;
using StoreAppModel;

namespace StoreAppUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IMenu appMenu = new StartMenu();
            bool repeat = true;
            MenuType currentMenu = MenuType.StartMenu;
            IFactory menuFactory = new MenuFactory();

            
            while (repeat)
            {
                //Console.Clear();
                appMenu.Menu();
                currentMenu = appMenu.YourChoice();

                switch (currentMenu)
                {
                    case MenuType.StartMenu:
                        appMenu = menuFactory.GetMenu(MenuType.StartMenu);
                        break;
                    case MenuType.CustomerMenu:
                        appMenu = menuFactory.GetMenu(MenuType.CustomerMenu);
                        break;
                    case MenuType.CustomerLogin:
                        appMenu = menuFactory.GetMenu(MenuType.CustomerLogin);
                        break;
                    case MenuType.PlaceOrderMenu:
                        appMenu = menuFactory.GetMenu(MenuType.PlaceOrderMenu);
                        break;
                    case MenuType.SearchCustomerMenu:
                        appMenu = menuFactory.GetMenu(MenuType.SearchCustomerMenu);
                        break;
                    case MenuType.ShowCustomerMenu:
                        appMenu = menuFactory.GetMenu(MenuType.ShowCustomerMenu);
                        break;
                    case MenuType.AddCustomerMenu:
                        appMenu = menuFactory.GetMenu(MenuType.AddCustomerMenu);
                        break;
                    case MenuType.ManagerLogin:
                        appMenu = menuFactory.GetMenu(MenuType.ManagerLogin);
                        break;
                    case MenuType.ManagerMenu:
                        appMenu = menuFactory.GetMenu(MenuType.ManagerMenu);
                        break;
                    case MenuType.Exit:
                        repeat = false;
                        Console.WriteLine(ASCII.ProgramExit);
                        break;
                    default:
                        Console.WriteLine("Cannot process what you just entered!\nPlease press Enter and try again");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
