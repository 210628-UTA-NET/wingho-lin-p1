using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using StoreAppBL;
using Serilog;

namespace StoreAppWebUI.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreAppBL.ICustomerBL _custBL;
        
        public StoreController(ICustomerBL p_custBL)
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
        public IActionResult View(int p_StoreFrontID)
        {
            StoreAppModel.StoreFront store = _custBL.GetStoreFrontByID(p_StoreFrontID);
            
            return View(new StoreAppWebUI.Models.StoreVM(_custBL.GetStoreFrontByID(p_StoreFrontID)));
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
        
        public IActionResult OrderByPriceAsc(int p_StoreID)
        {
            List<StoreAppModel.Order> custOrders = _custBL.GetStoreFrontOrdersSortedByCostAsc(p_StoreID);

            return View(new StoreAppWebUI.Models.OrderViewVM(custOrders));
        }

        public IActionResult OrderByPriceDesc(int p_StoreID)
        {
            List<StoreAppModel.Order> custOrders = _custBL.GetStoreFrontOrdersSortedByCostDesc(p_StoreID);

            return View(new StoreAppWebUI.Models.OrderViewVM(custOrders));
        }

        public IActionResult OrderByDateAsc(int p_StoreID)
        {
            List<StoreAppModel.Order> custOrders = _custBL.GetStoreFrontOrdersSortedByDateAsc(p_StoreID);

            return View(new StoreAppWebUI.Models.OrderViewVM(custOrders));
        }

        public IActionResult OrderByDateDesc(int p_StoreID)
        {
            List<StoreAppModel.Order> custOrders = _custBL.GetStoreFrontOrdersSortedByDateDesc(p_StoreID);

            return View(new StoreAppWebUI.Models.OrderViewVM(custOrders));
        }
    }
}