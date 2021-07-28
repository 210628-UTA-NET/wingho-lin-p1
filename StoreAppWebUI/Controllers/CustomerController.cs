using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using StoreAppBL;
using Serilog;

namespace StoreAppWebUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly StoreAppBL.ICustomerBL _customerBL;

        public CustomerController(ICustomerBL p_customerBL)
        {
            _customerBL = p_customerBL;
        }

        public IActionResult Index()
        {
            return View(
                _customerBL.GetAllCustomer()
                .Select(customer => new StoreAppWebUI.Models.CustomerVM(customer))
                .ToList()
            );
        }
        
        public IActionResult Add()
        {
            return View(new StoreAppWebUI.Models.CustomerVM());
        }
        public IActionResult AddCustomer(StoreAppWebUI.Models.CustomerVM p_customer)
        {
            if (ModelState.IsValid)
            {
                StoreAppModel.Customer customer = new StoreAppModel.Customer()
                {
                    CustomerName = p_customer.Name,
                    CustomerAddress = p_customer.Address,
                    CustomerPhone = p_customer.PhoneNumber,
                    CustomerEmail = p_customer.Email
                };
                try {
                    _customerBL.AddCustomer(customer);
                } catch(SystemException e)
                {
                    Log.Logger = new LoggerConfiguration().WriteTo
                        .File("log-.txt", rollingInterval: RollingInterval.Day)
                        .CreateLogger();
                    Log.Error("An error occured when adding a new user (Name: " + customer.CustomerName + ") to the database.");
                    Log.Error(e.ToString());
                    return RedirectToAction("Index");
                }
                Log.Logger = new LoggerConfiguration().WriteTo
                    .File("log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                Log.Information("New Customer added: Name:" + customer.CustomerName + ", Email: " + customer.CustomerEmail + ", Phone: " + customer.CustomerPhone);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Search(StoreAppWebUI.Models.CustomerVM p_customer)
        {
            return View(_customerBL.GetCustomersByName(p_customer.Name)
                .Select(customer => new StoreAppWebUI.Models.CustomerVM(customer))
                .ToList());
        }

        public IActionResult SearchPrompt()
        {
            return View();
        }

        public IActionResult OrderOptions(int p_custID)
        {
            return View(p_custID);
        }

        public IActionResult OrderByPriceAsc(int p_custID)
        {
            return View(_customerBL.GetCustomerOrdersSortedByCostAsc(p_custID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult OrderByPriceDesc(int p_custID)
        {
            return View(_customerBL.GetCustomerOrdersSortedByCostDesc(p_custID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult OrderByDateAsc(int p_custID)
        {
            return View(_customerBL.GetCustomerOrdersSortedByDateAsc(p_custID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult OrderByDateDesc(int p_custID)
        {
            return View(_customerBL.GetCustomerOrdersSortedByDateDesc(p_custID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }
    }
}