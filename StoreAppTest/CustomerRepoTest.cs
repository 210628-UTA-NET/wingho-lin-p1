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
                Assert.Equal(2, customers.Count);
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

                Customer customer = repo.GetCustomerByID(3);

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
        /// This seeds (2) Customer data entries into a database (should be clean).
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

                context.Customers.AddRange(
                    customer1,
                    customer2
                    );

                context.Orders.AddRange(
                        new Order
                        {
                            OrderID = 1,
                            OrderPrice = 2.00,
                            Customer = customer1,
                            StoreFront = new StoreFront
                            {
                                StoreFrontAddress = "234 Shoppers Street",
                                StoreFrontID = 1
                            }

                        },
                        new Order
                        {
                            OrderID = 2,
                            OrderPrice = 3.00,
                            Customer = customer1,
                            StoreFront = new StoreFront
                            {
                                StoreFrontAddress = "123 Disney Land",
                                StoreFrontID = 2
                            }

                        },
                        new Order
                        {
                            OrderID = 3,
                            OrderPrice = 100.00,
                            Customer = customer2,
                            StoreFront = new StoreFront
                            {
                                StoreFrontAddress = "1337 Wall Mart Avenue",
                                StoreFrontID = 3
                            }

                        }
                    );

                context.SaveChanges();
            }
        }
    }
}