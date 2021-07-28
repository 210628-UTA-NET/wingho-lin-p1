using System.Collections.Generic;
using StoreAppDL;
using StoreAppModel;

namespace StoreAppBL
{
    public class CustomerBL : ICustomerBL
    {
        /// <summary>
        /// This should provide a dependency for all of the methods that will interact with the database.
        /// </summary>
        private IRepository _repo;
        
        /// <summary>
        /// Constructor to bind and database context to BL instance
        /// </summary>
        /// <param name="p_repo">Repository context to interact with the database backend</param>
        public CustomerBL(IRepository p_repo)
        {
            _repo = p_repo;
        }

        public bool AddCustomer(Customer p_cust)
        {
            return _repo.AddCustomer(p_cust);
        }

        public List<Customer> GetAllCustomer()
        {
            return _repo.GetAllCustomer();
        }

        public List<LineItem> GetAllLineItems()
        {
            return _repo.GetAllLineItems();
        }

        public List<StoreFront> GetAllStoreFronts()
        {
            return _repo.GetAllStoreFronts();
        }

        public List<Order> GetCustomerOrders(int p_custID)
        {
            return _repo.GetCustomerOrders(p_custID);
        }

        public List<Product> GetStoreProducts(int p_storeID)
        {
            return _repo.GetStoreProducts(p_storeID);
        }

        public List<int> GetStoreProductIDs(int p_storeID)
        {
            return _repo.GetStoreProductIDs(p_storeID);
        }
        public List<Product> GetAllStoreInventory()
        {
            return _repo.GetAllStoreInventory();
        }

        public List<Order> GetStoreFrontOrders(int p_storeID)
        {
            return _repo.GetStoreFrontOrders(p_storeID);
        }

        public List<Manager> GetAllManagers()
        {
            return _repo.GetAllManagers();
        }

        public bool UpdateInventory(int p_productID, int addedQuantity)
        {
            return _repo.UpdateInventory(p_productID, addedQuantity);
        }

        public bool PlaceOrder(StoreAppModel.Order p_order, double p_price, int p_custID)
        {
            return _repo.PlaceOrder(p_order, p_price, p_custID);
        }
        public string GetStoreLocation(int p_storeID)
        {
            return _repo.GetStoreLocation(p_storeID);
        }

        public Customer GetCustomerByID(int p_custID)
        {
            return _repo.GetCustomerByID(p_custID);
        }

        public Product GetProductByID(int p_productID)
        {
            return _repo.GetProductByID(p_productID);
        }

        public StoreFront GetStoreFrontByID(int p_storeFrontID)
        {
            return _repo.GetStoreFrontByID(p_storeFrontID);
        }

        public List<LineItem> GetLineItemsByOrderID(int p_orderID)
        {
            return _repo.GetLineItemsByOrderID(p_orderID);
        }

        public Order GetOrderByID(int p_orderID)
        {
            return _repo.GetOrderByID(p_orderID);
        }

        public List<Customer> GetCustomersByName(string p_name)
        {
            return _repo.GetCustomersByName(p_name);
        }

        public List<Order> GetCustomerOrdersSortedByCostAsc(int p_custID)
        {
            return _repo.GetCustomerOrdersSortedByCostAsc(p_custID);
        }

        public List<Order> GetCustomerOrdersSortedByCostDesc(int p_custID)
        {
            return _repo.GetCustomerOrdersSortedByCostDesc(p_custID);
        }

        public List<Order> GetStoreFrontOrdersSortedByCostAsc(int p_storeID)
        {
            return _repo.GetStoreFrontOrdersSortedByCostAsc(p_storeID);
        }

        public List<Order> GetStoreFrontOrdersSortedByCostDesc(int p_storeID)
        {
            return _repo.GetStoreFrontOrdersSortedByCostDesc(p_storeID);
        }

        public List<Order> GetCustomerOrdersSortedByDateAsc(int p_custID)
        {
            return _repo.GetCustomerOrdersSortedByDateAsc(p_custID);
        }

        public List<Order> GetCustomerOrdersSortedByDateDesc(int p_custID)
        {
            return _repo.GetCustomerOrdersSortedByDateDesc(p_custID);
        }

        public List<Order> GetStoreFrontOrdersSortedByDateAsc(int p_storeID)
        {
            return _repo.GetStoreFrontOrdersSortedByDateAsc(p_storeID);
        }

        public List<Order> GetStoreFrontOrdersSortedByDateDesc(int p_storeID)
        {
            return _repo.GetStoreFrontOrdersSortedByDateDesc(p_storeID);
        }

        public Manager GetManagerByID(int p_manID)
        {
            return _repo.GetManagerByID(p_manID);
        }

        public LineItem AddLineItem(LineItem p_lineItem)
        {
            return _repo.AddLineItem(p_lineItem);
        }

        public int GetNextOrderID()
        {
            return _repo.GetNextOrderID();
        }

        public LineItem GetLineItemByID(int p_lineItemID)
        {
            return _repo.GetLineItemByID(p_lineItemID);
        }
    }
}