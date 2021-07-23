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
        /// Adds a customer to the database
        /// </summary>
        /// <param name="p_cust">This is the customer object that will be added to the database</param>
        /// <returns>Will return the customer object we just added</returns>
        Customer AddCustomer(Customer p_cust);

        List<StoreFront> GetAllStoreFronts();

        List<Order> GetCustomerOrders(int p_custID);

        /// <summary>
        /// Gets all data from the LineItems table, which is the order history.
        /// </summary>
        /// <returns>Returns a List of Lineitems from previously placed orders.</returns>
        List<LineItem> GetAllLineItems();

        List<Product> GetStoreProducts(int p_storeID);

        List<int> GetStoreProductIDs(int p_storeID);
        List<Product> GetAllStoreInventory();

        List<Order> GetStoreFrontOrders(int p_storeID);
        List<Manager> GetAllManagers();

        bool UpdateInventory(int p_productID, int addedQuantity);
        bool PlaceOrder(StoreAppModel.Order p_order, double p_price, int p_custID);

        string GetStoreLocation(int p_storeID);
    }
}
