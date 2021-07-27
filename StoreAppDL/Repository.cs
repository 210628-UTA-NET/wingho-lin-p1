using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using StoreAppModel;
using System.Linq;

namespace StoreAppDL
{
    public class Repository : IRepository
    {
        private StoreAppDBContext _context;
        
        public Repository(StoreAppDBContext p_context)
        {
            _context = p_context;
        }
        public bool AddCustomer(Customer p_cust)
        {
            try
            {
                _context.Customers.Add(p_cust);
                _context.SaveChanges();
            } catch (System.Exception)
            {
                return false;
            }
            
            return true;
        }

        public List<Customer> GetAllCustomer()
        {
            return _context.Customers.Select(cust => cust).ToList();
        }

        public Customer GetCustomerByID(int p_custID)
        {
            return _context.Customers.Find(p_custID);
        }

        public List<LineItem> GetAllLineItems()
        {
            return _context.LineItems.Select(li => li).ToList();
        }

        public List<Manager> GetAllManagers()
        {
            return _context.Managers.Select(man => man).ToList();
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _context.StoreFronts.Select(sf => sf).ToList();

        }

        public List<Product> GetAllStoreInventory()
        {
            return _context.Products.Select(prods => prods).ToList();
        }

        public List<Order> GetCustomerOrders(int p_custID)
        {
            List<Order> CustomerOrders = new List<Order>();
            CustomerOrders = _context.Orders
                .Where(ord => ord.Customer.CustomerID == p_custID)
                .Select(ord => ord).ToList();

            return CustomerOrders;
        }

        public List<Order> GetStoreFrontOrders(int p_storeID)
        {
            
            List<Order> StoreOrders = new List<Order>();
            StoreOrders = _context.Orders
                .Where(ord => ord.Customer.CustomerID == p_storeID)
                .Select(ord => ord).ToList();

            return StoreOrders;
        }

        public List<Product> GetStoreProducts(int p_storeID)
        {
            List<Product> StoreProducts = new List<Product>();
            StoreProducts = _context.Products
                .Where(prod => prod.StoreFront.StoreFrontID == p_storeID)
                .Select(prod => prod).ToList();

            return StoreProducts;
        }

        public List<int> GetStoreProductIDs(int p_storeID)
        {
            List<int> productIDs = new List<int>();

            List<Product> AllProducts = GetStoreProducts(p_storeID);
            foreach (Product product in AllProducts)
            {
                productIDs.Add(product.ProductID);
            }

            return productIDs;
        }

        public bool UpdateInventory(int p_productID, int addedQuantity)
        {
            try
            {
                var item = _context.Products.Find(p_productID);
                
                item.ProductQuantity += addedQuantity;
                _context.Products.Update(item);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool PlaceOrder(Order p_order, double p_price, int p_custID)
        {
            try
            {
                _context.Orders.Add(p_order);
                _context.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public string GetStoreLocation(int p_storeID)
        {
            string location = "";
            StoreFront Store = _context.StoreFronts.Find(p_storeID);
            location = Store.StoreFrontAddress;

            return location;
        }
    }
}