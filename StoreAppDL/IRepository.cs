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

        /// <summary>
        /// Updates Product quantities, which serves as a StoreFront's inventory.
        /// </summary>
        /// <param name="p_productID">The Product ID of the Product's whose quantity should be changed</param>
        /// <param name="addedQuantity">The amount that the quantity should be changed by</param>
        /// <returns>true if successful, false if an exception was caught while performing the database change</returns>
        bool UpdateInventory(int p_productID, int addedQuantity);

        /// <summary>
        /// Adds a Customer's order to the database
        /// </summary>
        /// <param name="p_order">The order that will be added</param>
        /// <param name="p_price">The Price of the order</param>
        /// <param name="p_custID">The customer ID to associate the order with</param>
        /// <returns>true if successful, false if an exception was caught while performing the database change</returns>
        bool PlaceOrder(Order p_order, double p_price, int p_custID);

        /// <summary>
        /// Gets StoreFront Location/Address
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID of the StoreFront to be searched for</param>
        /// <returns>The address of the StoreFront as a string</returns>
        string GetStoreLocation(int p_storeID);

        /// <summary>
        /// Gets a Product using a Product ID
        /// </summary>
        /// <param name="p_productID">The Product ID to be used to find a matching Product</param>
        /// <returns>Product object with a matching ProductID</returns>
        Product GetProductByID(int p_productID);

        /// <summary>
        /// Gets a StoreFront using a StoreFront ID
        /// </summary>
        /// <param name="p_storeFrontID">The StoreFront ID to be used to find a matching StoreFront</param>
        /// <returns>StoreFront objext with a matching StoreFront ID</returns>
        StoreFront GetStoreFrontByID(int p_storeFrontID);

        /// <summary>
        /// Gets LineItems that have a specified Order ID
        /// </summary>
        /// <param name="p_orderID">The Order ID to be used to find matching LineItems</param>
        /// <returns>List of LineItems that have the specified Order ID</returns>
        List<LineItem> GetLineItemsByOrderID(int p_orderID);

        /// <summary>
        /// Gets an Order with a specified Order ID
        /// </summary>
        /// <param name="p_orderID">The Order ID to be used to find a matching Order</param>
        /// <returns>Order objext with the specified Order ID</returns>
        Order GetOrderByID(int p_orderID);

        /// <summary>
        /// Searches for Customers by name
        /// </summary>
        /// <param name="p_name">The substring to be used when searching for Customer by name</param>
        /// <returns>List of Customers that have the specified (sub-string) name in their Name</returns>
        List<Customer> GetCustomersByName(string p_name);

        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in ascending order, based on Order Cost
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Ascending order</returns>
        List<Order> GetCustomerOrdersSortedByCostAsc(int p_custID);

        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in descending order, based on Order Cost
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Descending order</returns>
        List<Order> GetCustomerOrdersSortedByCostDesc(int p_custID);

        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in ascending order, based on Order Cost
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Ascending order</returns>
        List<Order> GetStoreFrontOrdersSortedByCostAsc(int p_storeID);

        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in descending order, based on Order Cost
        /// </summary>
        /// <param name="p_storeID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by Price in Descending order</returns>
        List<Order> GetStoreFrontOrdersSortedByCostDesc(int p_storeID);

        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in ascending order, based on DatePlaced
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Ascending order</returns>
        List<Order> GetCustomerOrdersSortedByDateAsc(int p_custID);

        /// <summary>
        /// Gets Orders for Customers based on a specified Customer ID and sorts in descending order, based on DatePlaced
        /// </summary>
        /// <param name="p_custID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Descending order</returns>
        List<Order> GetCustomerOrdersSortedByDateDesc(int p_custID);

        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in ascending order, based on DatePlaced
        /// </summary>
        /// <param name="p_storeID">The StoreFront ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Ascending order</returns>
        List<Order> GetStoreFrontOrdersSortedByDateAsc(int p_storeID);

        /// <summary>
        /// Gets Orders for StoreFronts based on a specified StoreFront ID and sorts in descending order, based on DatePlaced
        /// </summary>
        /// <param name="p_storeID">The Customer ID to be used to find a matching Order</param>
        /// <returns>List of Orders, sorted by DatePlaced in Descending order</returns>
        List<Order> GetStoreFrontOrdersSortedByDateDesc(int p_storeID);

        /// <summary>
        /// Gets a Manager with a matching ID
        /// </summary>
        /// <param name="p_manID">Manager ID to be matched with</param>
        /// <returns>Manager object with a matching Manager ID</returns>
        Manager GetManagerByID(int p_manID);

        LineItem AddLineItem(LineItem p_lineItem);

        int GetNextOrderID();

        LineItem GetLineItemByID(int p_lineItemID);
    }
}
