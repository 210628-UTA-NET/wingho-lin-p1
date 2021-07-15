using System.IO;
using StoreAppBL;
using StoreAppDL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreAppDL.Entities;

namespace StoreAppUI
{
    public class MenuFactory : IFactory
    {
        public IMenu GetMenu(MenuType p_menu)
        {
            //Get configuration from appsetting.json to connect to sql server
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
                .Build();

            //Grab connectionString from json file
            string connectionString = configuration.GetConnectionString("Reference2DB");

            DbContextOptions<WingDBContext> options = new DbContextOptionsBuilder<WingDBContext>()
                .UseSqlServer(connectionString)
                .Options;

            switch (p_menu)
            {
                case MenuType.StartMenu:
                    return new StartMenu();
                case MenuType.CustomerMenu:
                    return new CustomerMenu(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.CustomerLogin:
                    return new CustomerLogin(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.PlaceOrderMenu:
                    return new PlaceOrderMenu(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.ManagerMenu:
                    return new ManagerMenu(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.ManagerLogin:
                    return new ManagerLogin(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.ShowCustomerMenu:
                    return new ShowCustomerMenu(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.AddCustomerMenu:
                    return new AddCustomerMenu(new CustomerBL(new Repository(new WingDBContext(options))));
                case MenuType.SearchCustomerMenu:
                    return new SearchCustomerMenu(new CustomerBL(new Repository(new WingDBContext(options))));
                default:
                    return null;
            }
        }
    }
}