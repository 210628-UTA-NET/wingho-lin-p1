using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Model = StoreAppModel;
using Entity = StoreAppDL.Entities;
using System.Linq;
using StoreAppModel;

namespace StoreAppDL
{
    public class Repository : IRepository
    {
        private Entities.WingDBContext _context;
        
        public Repository(Entities.WingDBContext p_context)
        {
            _context = p_context;
        }
        public Model.Customer AddCustomer(Model.Customer p_cust)
        {
            _context.Customers.Add(new Entity.Customer{
                CustomerId = p_cust.ID,
                CustomerName = p_cust.Name,
                CustomerAddress = p_cust.Address,
                CustomerEmail = p_cust.Email,
                CustomerPhone = p_cust.Phone,
                CustomerPassword = p_cust.Password
            });

            _context.SaveChanges();
            return p_cust;
        }

        public List<Model.Customer> GetAllCustomer()
        {
            //Method Syntax way
            return _context.Customers.Select(
                cust => 
                    new Model.Customer()
                    {
                        ID = cust.CustomerId,
                        Name = cust.CustomerName,
                        Address = cust.CustomerAddress,
                        Email = cust.CustomerEmail,
                        Phone = cust.CustomerPhone,
                        Password = cust.CustomerPassword
                    }
            ).ToList();
        }

        public List<Model.LineItem> GetAllLineItems()
        {
            List<Model.LineItem> AllLineItems = new List<Model.LineItem>();
            AllLineItems = _context.LineItems.Select(
                li => 
                    new Model.LineItem()
                    {
                        ProductID = (int)li.ProductId,
                        Quantity = (int)li.LineItemQuantity
                    }
            ).ToList();

            return AllLineItems;
        }

        public List<Manager> GetAllManagers()
        {
            return _context.Managers.Select(
                man => 
                    new Model.Manager()
                    {
                        ID = man.ManagerId,
                        Name = man.ManagerName,
                        Username = man.ManagerUsername,
                        StoreID = (int)man.StoreId,
                        Password = man.ManagerPassword
                    }
            ).ToList();
        }

        public List<Model.StoreFront> GetAllStoreFronts()
        {
            List<Model.StoreFront> AllStoreFronts = new List<Model.StoreFront>();
            AllStoreFronts = _context.StoreFronts.Select(
                sf => 
                    new Model.StoreFront()
                    {
                        Name = sf.StoreFrontName,
                        Address = sf.StoreFrontAddress,
                        StoreFrontID = sf.StoreFrontId
                    }
            ).ToList();

            return AllStoreFronts;
        }

        public List<Model.LineItem> GetAllStoreInventory()
        {
            List<Model.LineItem> AllInventory = new List<Model.LineItem>();
            AllInventory = _context.Inventories.Select(
                inven => 
                    new Model.LineItem()
                    {
                        ProductID = (int)inven.ProductId,
                        Quantity = (int)inven.InventoryQuantity
                    }
            ).ToList();

            return AllInventory;
        }

        public List<Model.Order> GetCustomerOrders(int p_custID)
        {
            List<Model.Order> AllOrders = new List<Model.Order>();
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

            List<Model.Order> CustomerOrders = new List<Model.Order>();
            foreach (Model.Order order in AllOrders)
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

        public List<Model.LineItem> GetStoreInventory(int p_storeID)
        {
            List<Model.LineItem> Inventory = new List<Model.LineItem>();
            List<Model.LineItem> AllInventory = GetAllStoreInventory();
            List<Model.Product> AllProducts = GetStoreProducts(p_storeID);
            List<int> MatchingProductIDs = new List<int>();

            foreach (Model.Product item in AllProducts)
            {
                if (item.StoreID == p_storeID)
                {
                    MatchingProductIDs.Add(item.ProductID);
                }
            }

            foreach (Model.LineItem item in AllInventory)
            {
                if (MatchingProductIDs.Contains(item.ProductID))
                {
                    Inventory.Add(item);
                }
            }

            //populate Name property
            foreach (Model.LineItem item in Inventory)
            {
                foreach (Model.Product product in AllProducts)
                {
                    if (item.ProductID == product.ProductID)
                    {
                        item.ProductName = product.Name;
                        break;
                    }
                }
            }

            return Inventory;
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
                        StoreID = (int)prod.StoreId
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

        public bool ReplenishInventory(int p_productID, int addedQuantity)
        {
            try
            {
                var results = _context.Inventories.Where(p => p.ProductId == p_productID);
                //_context.Attach(results);
                foreach (var result in results)
                {
                    _context.Attach(result);
                    result.InventoryQuantity += addedQuantity;
                    //Console.WriteLine("Inside replenish inv()"+ result.InventoryQuantity.ToString());
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

                _context.CustomerOrders.Add(customerOrder);
                _context.SaveChanges();

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