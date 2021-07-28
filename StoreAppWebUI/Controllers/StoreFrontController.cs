using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using StoreAppBL;
using Serilog;

namespace StoreAppWebUI.Controllers
{
    public class StoreFrontController : Controller
    {
        private readonly StoreAppBL.ICustomerBL _custBL;
        
        public StoreFrontController(ICustomerBL p_custBL)
        {
            _custBL = p_custBL;
        }

        public IActionResult Index()
        {
            return View(
                _custBL.GetAllStoreFronts()
                .Select(sf => new StoreAppWebUI.Models.StoreVM(sf))
                .ToList()
            );
        }
        public IActionResult View(int p_storeID)
        {
            return View(_custBL.GetStoreProducts(p_storeID)
                .Select(prod => new StoreAppWebUI.Models.ProductVM(prod))
                .ToList());
        }

        public IActionResult UpdateQuantity(StoreAppWebUI.Models.ProductVM p_product, int p_addQuantity)
        {
            StoreAppModel.Product product = new StoreAppModel.Product()
            {
                ProductID = p_product.ID,
                ProductName = p_product.Name,
                ProductQuantity = p_product.Quantity
            };

           bool attempt = _custBL.UpdateInventory(product.ProductID, p_addQuantity);

           return RedirectToAction("Index");

        }
        
        public IActionResult OrderOptions(int p_storeID)
        {
            return View(p_storeID);
        }

        public IActionResult OrderByPriceAsc(int p_storeID)
        {
            return View(_custBL.GetStoreFrontOrdersSortedByCostAsc(p_storeID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult OrderByPriceDesc(int p_storeID)
        {
            return View(_custBL.GetStoreFrontOrdersSortedByCostDesc(p_storeID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult OrderByDateAsc(int p_storeID)
        {
            return View(_custBL.GetStoreFrontOrdersSortedByDateAsc(p_storeID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult OrderByDateDesc(int p_storeID)
        {
            return View(_custBL.GetStoreFrontOrdersSortedByDateDesc(p_storeID)
                .Select(order => new StoreAppWebUI.Models.OrderVM(order))
                .ToList());
        }

        public IActionResult Replenish()
        {
            return View();
        }

        public IActionResult OrderDetails(int p_orderID)
        {
            return View(_custBL.GetLineItemsByOrderID(p_orderID)
                .Select(li => new StoreAppWebUI.Models.LineItemVM(li))
                .ToList());
        }
    }
}