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

        /// <summary>
        /// Adds a customer to the database
        /// </summary>
        /// <param name="p_cust">This is the customer object that will be added to the database</param>
        /// <returns>Will return the customer object we just added</returns>
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

        /// <summary>
        /// Gets a list of Customers stored in our database
        /// </summary>
        /// <returns>Returns a list of Customers</returns>
        public List<Customer> GetAllCustomer()
        {
            return _context.Customers.Select(cust => cust).ToList();
        }

        /// <summary>
        /// Returns Customer info given a CustomerID
        /// </summary>
        /// <param name="p_custID">The Customer ID to be matched</param>
        /// <returns>Customer object from the backend database that matches the customer ID</returns>
        public Customer GetCustomerByID(int p_custID)
        {
            return _context.Customers.Find(p_custID);
        }

        /// <summary>
        /// Gets all data from the LineItems table, which is the order history.
        /// </summary>
        /// <returns>Returns a List of Lineitems from previously placed orders.</returns>
        public List<LineItem> GetAllLineItems()
        {
            return _context.LineItems.Select(li => li).ToList();
        }

        /// <summary>
        /// Gets all Managers from the database
        /// </summary>
        /// <returns>List of all Products from every StoreFront</returns>
        public List<Manager> GetAllManagers()
        {
            return _context.Managers.Select(man => man).ToList();
        }

        /// <summary>
        /// Gets all StoreFronts stored in the database
        /// </summary>
        /// <returns>List of all StoreFronts</returns>
        public List<StoreFront> GetAllStoreFronts()
        {
            return _context.StoreFronts.Select(sf => sf).ToList();

        }

        /// <summary>
        /// Gets all Inventory from every store, which is located in the Products table
        /// </summary>
        /// <returns>List of all Products from every StoreFront</returns>
        public List<Product> GetAllStoreInventory()
        {
            return _context.Products.Select(prods => prods).ToList();
        }

        /// <summary>
        /// Gets all Orders with a matching CustomerID field
        /// </summary>
        /// <param name="p_custID">The Customer ID to be matched with</param>
        /// <returns>List of Orders corresponding to the specified Customer ID</returns>
        public List<Order> GetCustomerOrders(int p_custID)
        {
            List<Order> CustomerOrders = new List<Order>();
            CustomerOrders = _context.Orders
                .Where(ord => ord.Customer.CustomerID == p_custID)
                .Select(ord => ord).ToList();

            return CustomerOrders;
        }

        /// <summary>
        /// Gets all Orders with a matching StoreFrontID field
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be matched with</param>
        /// <returns>List of Orders corresponding to the specified StoreFront ID</returns>
        public List<Order> GetStoreFrontOrders(int p_storeID)
        {
            
            List<Order> StoreOrders = new List<Order>();
            StoreOrders = _context.Orders
                .Where(ord => ord.Customer.CustomerID == p_storeID)
                .Select(ord => ord).ToList();

            return StoreOrders;
        }

        /// <summary>
        /// Gets all Products with a matching StoreFrontID field
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be matched with</param>
        /// <returns>List of Products corresponding to the specified StoreFront ID</returns>
        public List<Product> GetStoreProducts(int p_storeID)
        {
            List<Product> StoreProducts = new List<Product>();
            StoreProducts = _context.Products
                .Where(prod => prod.StoreFront.StoreFrontID == p_storeID)
                .Select(prod => prod).ToList();

            return StoreProducts;
        }

        /// <summary>
        /// Gets all Product IDs from a specific StoreFront
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be matched with</param>
        /// <returns>List of int of ProductIDs corresponding to the specified StoreFront ID</returns>
        public List<int> GetStoreProductIDs(int p_storeID)
        {
            return _context.Products.Where(prod => prod.StoreFrontID == p_storeID).Select(prod => prod.ProductID).ToList();
        }

        /// <summary>
        /// Gets a Product using a Product ID
        /// </summary>
        /// <param name="p_productID">The Product ID to be used to find a matching Product</param>
        /// <returns>Product object with a matching ProductID</returns>
        public Product GetProductByID(int p_productID)
        {
            return _context.Products.Find(p_productID); ;
        }

        /// <summary>
        /// Updates Product quantities, which serves as a StoreFront's inventory.
        /// </summary>
        /// <param name="p_productID">The Product ID of the Product's whose quantity should be changed</param>
        /// <param name="addedQuantity">The amount that the quantity should be changed by</param>
        /// <returns>true if successful, false if an exception was caught while performing the database change</returns>
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

        /// <summary>
        /// Adds a Customer's order to the database
        /// </summary>
        /// <param name="p_order">The order that will be added</param>
        /// <param name="p_price">The Price of the order</param>
        /// <param name="p_custID">The customer ID to associate the order with</param>
        /// <returns>true if successful, false if an exception was caught while performing the database change</returns>
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
        
        /// <summary>
        /// Gets StoreFront Location/Address
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID of the StoreFront to be searched for</param>
        /// <returns>The address of the StoreFront as a string</returns>
        public string GetStoreLocation(int p_storeID)
        {
            string location = "";
            StoreFront Store = _context.StoreFronts.Find(p_storeID);
            location = Store.StoreFrontAddress;

            return location;
        }

        /// <summary>
        /// Gets a StoreFront using a StoreFront ID
        /// </summary>
        /// <param name="p_storeFrontID">The StoreFront ID to be used to find a matching StoreFront</param>
        /// <returns>StoreFront objext with a matching StoreFront ID</returns>
        public StoreFront GetStoreFrontByID(int p_storeFrontID)
        {
            return _context.StoreFronts.Find(p_storeFrontID);
        }

        /// <summary>
        /// Gets LineItems that have a specified Order ID
        /// </summary>
        /// <param name="p_orderID">The Order ID to be used to find matching LineItems</param>
        /// <returns>List of LineItems that have the specified Order ID</returns>
        public List<LineItem> GetLineItemsByOrderID(int p_orderID)
        {
            return _context.LineItems.Where(li => li.Order.OrderID == p_orderID).Select(li => li).ToList();
        }

        /// <summary>
        /// Gets an Order with a specified Order ID
        /// </summary>
        /// <param name="p_orderID">The Order ID to be used to find a matching Order</param>
        /// <returns>Order objext with the specified Order ID</returns>
        public Order GetOrderByID(int p_orderID)
        {
            return _context.Orders.Find(p_orderID);
        }

        /// <summary>
        /// Searches for Customers by name
        /// </summary>
        /// <param name="p_name">The substring to be used when searching for Customer by name</param>
        /// <returns>List of Customers that have the specified (sub-string) name in their Name</returns>
        public List<Customer> GetCustomersByName(string p_name)
        {
            return _context.Customers.Where(cust => cust.CustomerName.Contains(p_name)).Select(cust => cust).ToList();
        }

        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in ascending order, based on Order Cost
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Ascending order</returns>
        public List<Order> GetCustomerOrdersSortedByCostAsc(int p_custID)
        {
            List<Order> orders;

            orders = _context.Orders.Where(ord => ord.Customer.CustomerID == p_custID)
                .OrderBy(ord => ord.OrderPrice).Select(ord => ord).ToList();

            return orders;
        }
        
        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in descending order, based on Order Cost
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Descending order</returns>
        public List<Order> GetCustomerOrdersSortedByCostDesc(int p_custID)
        {
            return _context.Orders.Where(ord => ord.Customer.CustomerID == p_custID)
                .OrderByDescending(ord => ord.OrderPrice).Select(ord => ord).ToList();
        }
        
        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in ascending order, based on Order Cost
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Ascending order</returns>
        public List<Order> GetStoreFrontOrdersSortedByCostAsc(int p_storeID)
        {
            return _context.Orders.Where(ord => ord.StoreFront.StoreFrontID == p_storeID)
                .OrderBy(ord => ord.OrderPrice).Select(ord => ord).ToList();
        }
        
        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in descending order, based on Order Cost
        /// </summary>
        /// <param name="p_storeID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Descending order</returns>
        public List<Order> GetStoreFrontOrdersSortedByCostDesc(int p_storeID)
        {
            return _context.Orders.Where(ord => ord.StoreFront.StoreFrontID == p_storeID)
                .OrderByDescending(ord => ord.OrderPrice).Select(ord => ord).ToList();
        }
        
        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in ascending order, based on DatePlaced
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Ascending order</returns>
        public List<Order> GetCustomerOrdersSortedByDateAsc(int p_custID)
        {
            return _context.Orders.Where(ord => ord.Customer.CustomerID == p_custID)
                .OrderBy(ord => ord.DatePlaced).Select(ord => ord).ToList();
        }
        
        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in descending order, based on DatePlaced
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Descending order</returns>
        public List<Order> GetCustomerOrdersSortedByDateDesc(int p_custID)
        {
            return _context.Orders.Where(ord => ord.Customer.CustomerID == p_custID)
                .OrderByDescending(ord => ord.DatePlaced).Select(ord => ord).ToList();
        }
        
        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in ascending order, based on DatePlaced
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Ascending order</returns>
        public List<Order> GetStoreFrontOrdersSortedByDateAsc(int p_storeID)
        {
            return _context.Orders.Where(ord => ord.StoreFront.StoreFrontID == p_storeID)
                .OrderBy(ord => ord.DatePlaced).Select(ord => ord).ToList();
        }
        
        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in descending order, based on DatePlaced
        /// </summary>
        /// <param name="p_storeID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Descending order</returns>
        public List<Order> GetStoreFrontOrdersSortedByDateDesc(int p_storeID)
        {
            return _context.Orders.Where(ord => ord.StoreFront.StoreFrontID == p_storeID)
                .OrderByDescending(ord => ord.DatePlaced).Select(ord => ord).ToList();
        }

        /// <summary>
        /// Gets a Manager with a matching ID
        /// </summary>
        /// <param name="p_manID">Manager ID to be matched with</param>
        /// <returns>Manager object with a matching Manager ID</returns>
        public Manager GetManagerByID(int p_manID)
        {
            return _context.Managers.Find(p_manID);
        }

        /// <summary>
        /// Adds a LineItem to LineItems table in the database
        /// </summary>
        /// <param name="p_lineItem">The LineItem to be added</param>
        /// <returns>The LineItem object that was added</returns>
        public LineItem AddLineItem(LineItem p_lineItem)
        {
            _context.LineItems.Add(p_lineItem);
            _context.SaveChanges();

            return p_lineItem;
        }

        /// <summary>
        /// Gets the next available ID for the Orders table
        /// </summary>
        /// <returns>The next available ID for Orders as an int</returns>
        public int GetNextOrderID()
        {
            return _context.Orders.Select(ord => ord.OrderID).Count() + 1;
        }

        /// <summary>
        /// Gets LineItem by ID
        /// </summary>
        /// <param name="p_lineItemID">LineItem ID to be matched</param>
        /// <returns>LineItem object with a matching LineItem ID</returns>
        public LineItem GetLineItemByID(int p_lineItemID)
        {
            return _context.LineItems.Find(p_lineItemID);
        }
    }
}