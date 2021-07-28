using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using StoreAppDL;
using StoreAppBL;
using StoreAppModel;

namespace StoreAppTest
{
    public class CustomerRepoTest
    {
        private readonly DbContextOptions<StoreAppDBContext> _options;

        /// <summary>
        /// Initialize a test object. Database stored in a separate file from main application db
        /// and data is seeded to have controlled tests.
        /// </summary>
        public CustomerRepoTest()
        {
            _options = new DbContextOptionsBuilder<StoreAppDBContext>().UseSqlite("Filename = CustomerTest.db").Options;
            this.Seed();
        }

        /// <summary>
        /// This should attempt to retrieve all seeded customers from a clean, seeded database.
        /// Currently 2 customers are seeded.
        /// </summary>
        [Fact]
        public void GetAllCustomersShouldReturnAllCustomers()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                //Arrange
                IRepository repo = new Repository(context);
                List<Customer> customers;
                    
                //Act
                customers = repo.GetAllCustomer();

                //Assert
                Assert.NotNull(customers);
                Assert.Equal(3, customers.Count);
            }
        }

        /// <summary>
        /// This should attempt to retrieve customer with CustomerID of (1) from clean, seeded database.
        /// </summary>
        [Fact]
        public void GetCustomerShouldReturnACustomer()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                Customer customer = repo.GetCustomerByID(1);

                Assert.NotNull(customer);
                Assert.Equal("Wing Lin", customer.CustomerName);
                Assert.Equal("123 Going Up Street", customer.CustomerAddress);
                Assert.Equal("wingho.lin@revature.net", customer.CustomerEmail);
                Assert.Equal("1231231234", customer.CustomerPhone);
                Assert.Equal(1, customer.CustomerID);
            }
        }

        /// <summary>
        /// This tests if adding a customer to the database has the added customer persisting in the database.
        /// </summary>
        [Fact]
        public void AddCustomerShouldPersist()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                repo.AddCustomer(new Customer
                    {
                        CustomerName = "Bob",
                        CustomerAddress = "222 Even Street",
                        CustomerEmail = "bob@gmail.com",
                        CustomerPhone = "2222444466",
                        CustomerPassword = "passwor",
                    });

                Customer customer = repo.GetCustomerByID(4);

                Assert.NotNull(customer);
                Assert.Equal("Bob", customer.CustomerName);
                Assert.Equal("222 Even Street", customer.CustomerAddress);
                Assert.Equal("bob@gmail.com", customer.CustomerEmail);
                Assert.Equal("2222444466", customer.CustomerPhone);
            }
        }

        /// <summary>
        /// This retrieves all customer orders based on CustomerID (1)
        /// </summary>
        [Fact]
        public void GetCustomerOrdersShouldReturnCustomerOrders()
        {
            using(var context = new StoreAppDBContext(_options))
            {
                Customer customer;
                List<Order> orders;
                IRepository repo = new Repository(context);

                customer = repo.GetCustomerByID(1);
                orders = repo.GetCustomerOrders(customer.CustomerID);

                Assert.NotNull(orders);
                Assert.Equal(2, orders.Count);
            }
            
        }
        
        /// <summary>
        /// This tests if placing multiple orders persists in the database.
        /// </summary>
        [Fact]
        public void PlaceOrderShouldPersist()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                Customer customer = repo.GetCustomerByID(2);  // customer (2) already has 1 order in the db
                StoreFront storeFront = repo.GetStoreFrontByID(1);
                List<Order> orders = new List<Order>();
                Order order1 = new Order
                {
                    OrderPrice = 300.00,
                    Customer = customer,
                    StoreFront = storeFront,
                    DatePlaced = DateTime.Now
                };
                Order order2 = new Order
                {
                    OrderPrice = 50.00,
                    Customer = customer,
                    StoreFront = storeFront,
                    DatePlaced = DateTime.Now
                };

                repo.PlaceOrder(order1, order1.OrderPrice, customer.CustomerID);
                repo.PlaceOrder(order2, order2.OrderPrice, customer.CustomerID);
                orders = repo.GetCustomerOrders(customer.CustomerID);

                Assert.NotNull(orders);
                Assert.Equal(3, orders.Count);
            }
        }

        /// <summary>
        /// This should get a List of LineItems by OrderID.
        /// </summary>
        [Fact]
        public void GetLineItemsByOrderIDShouldReturnLineItems()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                Order order = repo.GetOrderByID(1);
                List<LineItem> lineItems;

                lineItems = repo.GetLineItemsByOrderID(order.OrderID);

                Assert.NotNull(lineItems);
                Assert.Equal(2, lineItems.Count);
            }
        }

        /// <summary>
        /// This gets a List of Customers by name.
        /// </summary>
        [Fact]
        public void GetCustomersByNameShouldReturnCustomers()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Customer> customers;

                customers = repo.GetCustomersByName("Wing");  // there should be 2 folks matching this

                Assert.NotNull(customers);
                Assert.Equal(2, customers.Count);
            }
        }

        /// <summary>
        /// This should return a List of all LineItems from the LineItems table.
        /// </summary>
        [Fact]
        public void GetAllLineItemsShouldReturnAllLineItems()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<LineItem> lineItems;

                lineItems = repo.GetAllLineItems();

                Assert.Equal(2, lineItems.Count);
            }
        }

        /// <summary>
        /// This tests if Customer Orders are returned sorted in ascending order by cost.
        /// </summary>
        [Fact]
        public void GetOrdersSortedByCostAscShouldReturnOrdersSortedByCostAsc()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Order> orders;

                orders = repo.GetCustomerOrdersSortedByCostAsc(1);

                Assert.Equal(3.00, orders[0].OrderPrice);
                Assert.Equal(18.00, orders[1].OrderPrice);
            }
        }

        /// <summary>
        /// This tests if Customer Orders are returned sorted in descending order by cost.
        /// </summary>
        [Fact]
        public void GetOrdersSortedByCostDescShouldReturnOrdersSortedByCostDesc()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Order> orders;

                orders = repo.GetCustomerOrdersSortedByCostDesc(1);

                Assert.Equal(3.00, orders[1].OrderPrice);
                Assert.Equal(18.00, orders[0].OrderPrice);
            }
        }

        /// <summary>
        /// This tests if Customer Orders are returned, sorted in ascending order by DatePlaced.
        /// </summary>
        [Fact]
        public void GetOrdersSortedByDateAscShouldReturnOrdersSortedByDateAsc()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Order> orders;

                orders = repo.GetCustomerOrdersSortedByDateAsc(1);

                Assert.Equal(new DateTime(2021, 1, 1), orders[0].DatePlaced);
                Assert.Equal(new DateTime(2021, 2, 1), orders[1].DatePlaced);
            }
        }

        /// <summary>
        /// This tests if Customer Orders are returned sorted in descending order by cost.
        /// </summary>
        [Fact]
        public void GetOrdersSortedByDateDescShouldReturnOrdersSortedByDateDesc()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Order> orders;

                orders = repo.GetCustomerOrdersSortedByDateDesc(1);

                Assert.Equal(new DateTime(2021, 1, 1), orders[1].DatePlaced);
                Assert.Equal(new DateTime(2021, 2, 1), orders[0].DatePlaced);
            }
        }

        /// <summary>
        /// This tests adding a LineItem to the LineItems table and checks that it persists
        /// </summary>
        [Fact]
        public void AddLineItemShouldPersist()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                LineItem lineItem = new LineItem
                {
                    LineItemQuantity = 3,
                    Order = repo.GetOrderByID(2),
                    Product = repo.GetProductByID(1)
                };

                lineItem = repo.AddLineItem(lineItem);
                lineItem = repo.GetLineItemByID(3);

                Assert.NotNull(lineItem);
                Assert.Equal(1, lineItem.Product.ProductID);
                Assert.Equal(3, lineItem.LineItemQuantity);
            }
        }

        /// <summary>
        /// This seeds 
        /// (3) Customer
        /// (3) StoreFronts
        /// (3) Orders
        /// (2) Products
        /// (2) LineItems
        /// into a database (should be clean).
        /// </summary>
        private void Seed()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                //This ensures that our inmemory database gets deleted everytime before another test case runs it
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                Customer customer1 = new Customer
                {
                    CustomerName = "Wing Lin",
                    CustomerAddress = "123 Going Up Street",
                    CustomerEmail = "wingho.lin@revature.net",
                    CustomerPhone = "1231231234",
                    CustomerPassword = "password"
                };
                Customer customer2 = new Customer
                {
                    CustomerName = "Samuel",
                    CustomerAddress = "321 Going Down Street",
                    CustomerEmail = "samuel@gmail.com",
                    CustomerPhone = "3213213214",
                    CustomerPassword = "password2"
                };
                Customer customer3 = new Customer
                {
                    CustomerName = "Wing Ho Lin",
                    CustomerAddress = "234 Going Somewhere Street",
                    CustomerEmail = "wing@gmail.com",
                    CustomerPhone = "3334444333",
                    CustomerPassword = "password"
                };

                context.Customers.AddRange(
                    customer1,
                    customer2,
                    customer3
                );

                StoreFront store1 = new StoreFront()
                {
                    StoreFrontName = "Awesome Toy Store",
                    StoreFrontAddress = "234 Shoppers Street",
                };
                StoreFront store2 = new StoreFront()
                {
                    StoreFrontName = "Land Disney",
                    StoreFrontAddress = "432 Backwards Street",
                };
                StoreFront store3 = new StoreFront()
                {
                    StoreFrontName = "Mart Wal",
                    StoreFrontAddress = "321 Backwards Street",
                };

                context.StoreFronts.AddRange(
                    store1,
                    store2,
                    store3
                );

                Order order1 = new Order
                {
                    OrderID = 1,
                    OrderPrice = 18.00,
                    Customer = customer1,
                    StoreFront = store1,
                    DatePlaced = new DateTime(2021, 1, 1)

                };
                Order order2 = new Order
                {
                    OrderID = 2,
                    OrderPrice = 3.00,
                    Customer = customer1,
                    StoreFront = store2,
                    DatePlaced = new DateTime(2021, 2, 1)

                };
                Order order3 = new Order
                {
                    OrderID = 3,
                    OrderPrice = 100.00,
                    Customer = customer2,
                    StoreFront = store3

                };
                context.Orders.AddRange(
                    order1,
                    order2,
                    order3
                );

                Product product1 = new Product
                {
                    ProductName = "Toy truck",
                    ProductPrice = 5.00,
                    ProductQuantity = 10,
                    StoreFront = store1
                };
                Product product2 = new Product
                {
                    ProductName = "Toy boat",
                    ProductPrice = 3.00,
                    ProductQuantity = 11,
                    StoreFront = store1
                };
                context.Products.AddRange(
                    product1,
                    product2
                );

                context.LineItems.AddRange(
                    new LineItem
                    {
                        LineItemQuantity = 3,
                        Order = order1,
                        Product = product1
                    },
                    new LineItem
                    {
                        LineItemQuantity = 1,
                        Order = order1,
                        Product = product2
                    }
                );

                context.SaveChanges();
            }
        }
    }
}