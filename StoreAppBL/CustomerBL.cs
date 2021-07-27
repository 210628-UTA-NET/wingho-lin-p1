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
    }
}