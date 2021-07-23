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
        public Customer AddCustomer(Customer p_cust)
        {
            _context.Customers.Add(p_cust);
            _context.SaveChanges();
            return p_cust;
        }

        public List<Customer> GetAllCustomer()
        {
            //Method Syntax way
            return _context.Customers.Select(cust => cust).ToList();
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
            List<Order> AllOrders = new List<Order>();
            AllOrders = _context.CustomerOrders.Select(ord => ord).ToList();

            List<Order> CustomerOrders = new();
            foreach (Order order in AllOrders)
            {
                if (order.CustomerID == p_custID)
                {
                    CustomerOrders.Add(order);
                }
            }

            return CustomerOrders;
        }

        public List<Order> GetStoreFrontOrders(int p_storeID)
        {
            string Location = "";

            List<StoreFront> StoreFronts = GetAllStoreFronts();
            foreach (StoreFront StoreFront in StoreFronts)
            {
                if (StoreFront.StoreFrontID == p_storeID)
                {
                    Location = StoreFront.Address;
                }
            }
            
            List<Order> AllOrders = new List<Order>();
            AllOrders = _context.CustomerOrders.Select(
                ord => 
                    new Model.Order()
                    {
                        Location = ord.StoreAddress,
                        TotalPrice = (double)ord.OrderPrice,
                        OrderID = ord.OrderId,
                        CustomerID = (int)ord.CustomerId
                    }
            ).ToList();

            List<Order> StoreOrders = new List<Order>();
            foreach (Order order in AllOrders)
            {
                if (order.Location == Location)
                {
                    StoreOrders.Add(order);
                }
            }

            return StoreOrders;
        }

        public List<Model.Product> GetStoreProducts(int p_storeID)
        {
            List<Model.Product> AllProducts = new List<Model.Product>();
            AllProducts = _context.Products.Select(
                prod => 
                    new Model.Product()
                    {
                        ProductID = prod.ProductId,
                        Name = prod.ProductName,
                        Price = (double)prod.ProductPrice,
                        StoreID = (int)prod.StoreId,
                        Quantity = (int)prod.ProductQuantity
                    }
            ).ToList();

            List<Model.Product> StoreProducts = new List<Model.Product>();
            foreach (Model.Product product in AllProducts)
            {
                if (product.StoreID == p_storeID)
                {
                    StoreProducts.Add(product);
                }
            }

            return StoreProducts;
        }

        public List<int> GetStoreProductIDs(int p_storeID)
        {
            List<int> productIDs = new List<int>();

            List<Model.Product> AllProducts = GetStoreProducts(p_storeID);
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
                var results = _context.Products.Where(p => p.ProductId == p_productID);
                foreach (var result in results)
                {
                    result.ProductQuantity += addedQuantity;
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool PlaceOrder(Model.Order p_order, double p_price, int p_custID)
        {
            try
            {
                Entity.CustomerOrder customerOrder = new Entity.CustomerOrder()
                {
                    StoreAddress = p_order.Location,
                    CustomerId = p_custID,
                    OrderPrice = (decimal?)p_price
                };
                _context.CustomerOrders.Attach(customerOrder);
                _context.CustomerOrders.Add(customerOrder);
                
                _context.SaveChanges();

                // Add each order line items to LineItem table
                foreach (LineItem lineItem in p_order.OrderLineItems)
                {
                    _context.LineItems.Add(new Entity.LineItem()
                    {
                        OrderId = customerOrder.OrderId,
                        ProductId = lineItem.ProductID,
                        LineItemQuantity = lineItem.Quantity
                    });
                }
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
            List<StoreFront> Stores = GetAllStoreFronts();
            foreach (StoreFront store in Stores)
            {
                if (p_storeID == store.StoreFrontID)
                {
                    location = store.Address;
                    break;
                }
            }

            return location;
        }
    }
}