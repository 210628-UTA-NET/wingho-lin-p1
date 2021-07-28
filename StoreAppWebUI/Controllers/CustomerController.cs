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
        public IActionResult Create()
        {
            return View();
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

        public IActionResult FindCustomer()
        {
            return View();
        }
        public IActionResult Display(StoreAppWebUI.Models.CustomerVM p_customer)
        {
            return View(new StoreAppWebUI.Models.CustomerVM(_customerBL.GetCustomerByID(p_customer.ID)));
        }

        [HttpPost]
        public IActionResult Search(StoreAppWebUI.Models.CustomerVM p_customer)
        {
            //List<StoreAppModel.Customer> customers = _customerBL.GetCustomersByName(p_custName);

            return View(_customerBL.GetCustomersByName(p_customer.Name)
                .Select(customer => new StoreAppWebUI.Models.CustomerVM(customer))
                .ToList());
        }

        public IActionResult SearchPrompt()
        {
            return View();
        }
    }
}