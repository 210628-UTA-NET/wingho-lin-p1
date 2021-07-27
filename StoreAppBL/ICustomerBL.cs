using System;
using System.Collections.Generic;
using StoreAppModel;

namespace StoreAppBL
{
    /// <summary>
    /// Handles all the business logic for the customer model
    /// They are in charge of further processing/ sanitizing/ further validation of data
    /// Any logic that is used to access the data is for the DL, anything else will be in BL
    /// </summary>
    public interface ICustomerBL
    {
        /// <summary>
        /// Gets all the customers from the database
        /// </summary>
        /// <returns>Returns a list of customers</returns>
        List<Customer> GetAllCustomer();

        /// <summary>
        /// Adds a customer to the database
        /// </summary>
        /// <param name="p_cust">This is the customer object that will be added to the database</param>
        /// <returns>Will return the customer object we just added</returns>
        bool AddCustomer(Customer p_cust);

        List<StoreFront> GetAllStoreFronts();

        List<Order> GetCustomerOrders(int p_custID);

        List<LineItem> GetAllLineItems();

        List<Product> GetStoreProducts(int p_storeID);

        List<int> GetStoreProductIDs(int p_storeID);
        List<Manager> GetAllManagers();
        
        List<Order> GetStoreFrontOrders(int p_storeID);

        bool UpdateInventory(int p_productID, int addedQuantity);
        bool PlaceOrder(StoreAppModel.Order p_order, double p_price, int p_custID);

        string GetStoreLocation(int p_storeID);
    }
}
