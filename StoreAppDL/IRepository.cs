using System;
using System.Collections.Generic;
using StoreAppModel;

namespace StoreAppDL
{
    /// <summary>
    /// It is responsible for accessing our database
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets a list of Customers stored in our database
        /// </summary>
        /// <returns>Returns a list of Customers</returns>
        List<Customer> GetAllCustomer();

        /// <summary>
        /// Returns Customer info given a CustomerID
        /// </summary>
        /// <param name="p_custID">The Customer ID to be matched</param>
        /// <returns>Customer object from the backend database that matches the customer ID</returns>
        Customer GetCustomerByID(int p_custID);

        /// <summary>
        /// Adds a customer to the database
        /// </summary>
        /// <param name="p_cust">This is the customer object that will be added to the database</param>
        /// <returns>Will return the customer object we just added</returns>
        bool AddCustomer(Customer p_cust);

        /// <summary>
        /// Gets all StoreFronts stored in the database
        /// </summary>
        /// <returns>List of all StoreFronts</returns>
        List<StoreFront> GetAllStoreFronts();

        /// <summary>
        /// Gets all Orders with a matching CustomerID field
        /// </summary>
        /// <param name="p_custID">The Customer ID to be matched with</param>
        /// <returns>List of Orders corresponding to the specified Customer ID</returns>
        List<Order> GetCustomerOrders(int p_custID);

        /// <summary>
        /// Gets all data from the LineItems table, which is the order history.
        /// </summary>
        /// <returns>Returns a List of Lineitems from previously placed orders.</returns>
        List<LineItem> GetAllLineItems();

        /// <summary>
        /// Gets all Products with a matching StoreFrontID field
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be matched with</param>
        /// <returns>List of Products corresponding to the specified StoreFront ID</returns>
        List<Product> GetStoreProducts(int p_storeID);

        /// <summary>
        /// Gets all Product IDs from a specific StoreFront
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be matched with</param>
        /// <returns>List of int of ProductIDs corresponding to the specified StoreFront ID</returns>
        List<int> GetStoreProductIDs(int p_storeID);

        /// <summary>
        /// Gets all Inventory from every store, which is located in the Products table
        /// </summary>
        /// <returns>List of all Products from every StoreFront</returns>
        List<Product> GetAllStoreInventory();

        /// <summary>
        /// Gets all Orders with a matching StoreFrontID field
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be matched with</param>
        /// <returns>List of Orders corresponding to the specified StoreFront ID</returns>
        List<Order> GetStoreFrontOrders(int p_storeID);

        /// <summary>
        /// Gets all Managers from the database
        /// </summary>
        /// <returns>List of all Products from every StoreFront</returns>
        List<Manager> GetAllManagers();

        bool UpdateInventory(int p_productID, int addedQuantity);
        bool PlaceOrder(StoreAppModel.Order p_order, double p_price, int p_custID);

        string GetStoreLocation(int p_storeID);

        Product GetProductByID(int p_productID);
        StoreFront GetStoreFrontByID(int p_storeFrontID);
        List<LineItem> GetLineItemsByOrderID(int p_orderID);
        Order GetOrderByID(int p_orderID);
        List<Customer> GetCustomersByName(string p_name);
        List<Order> GetCustomerOrdersSortedByCostAsc(int p_custID);
        List<Order> GetCustomerOrdersSortedByCostDesc(int p_custID);
        List<Order> GetStoreFrontOrdersSortedByCostAsc(int p_storeID);
        List<Order> GetStoreFrontOrdersSortedByCostDesc(int p_storeID);
        List<Order> GetCustomerOrdersSortedByDateAsc(int p_custID);
        List<Order> GetCustomerOrdersSortedByDateDesc(int p_custID);
        List<Order> GetStoreFrontOrdersSortedByDateAsc(int p_storeID);
        List<Order> GetStoreFrontOrdersSortedByDateDesc(int p_storeID);

        Manager GetManagerByID(int p_manID);
    }
}
