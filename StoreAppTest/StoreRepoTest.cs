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
    public class StoreRepoTest
    {
        private readonly DbContextOptions<StoreAppDBContext> _options;

        public StoreRepoTest()
        {
            _options = new DbContextOptionsBuilder<StoreAppDBContext>().UseSqlite("Filename = StoreTest.db").Options;
            this.Seed();
        }

        /// <summary>
        /// This should attempt to retrieve all seeded customers from a clean, seeded database.
        /// Currently 2 customers are seeded.
        /// </summary>
        [Fact]
        public void GetAllStoreFrontsShouldReturnAllStoreFronts()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                //Arrange
                IRepository repo = new Repository(context);
                List<StoreFront> storeFronts;

                //Act
                storeFronts = repo.GetAllStoreFronts();

                //Assert
                Assert.NotNull(storeFronts);
                Assert.Equal(2, storeFronts.Count);
            }
        }

        

        

        /// <summary>
        /// This retrieves all customer orders based on CustomerID (1)
        /// </summary>
        [Fact]
        public void GetStoreFrontOrdersShouldReturnStoreFrontOrders()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                List<Order> orders;
                IRepository repo = new Repository(context);

                orders = repo.GetStoreFrontOrders(2);

                Assert.NotNull(orders);
                Assert.Single(orders);
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

                StoreFront store1 = new StoreFront()
                {
                    StoreFrontName = "Awesome Toy Store",
                    StoreFrontAddress = "321 Lego Avenue",
                };
                StoreFront store2 = new StoreFront()
                {
                    StoreFrontName = "Decent Toy Store",
                    StoreFrontAddress = "333 Lego Avenue",
                };

                context.StoreFronts.AddRange(
                    store1,
                    store2
                );

                context.Orders.AddRange(
                        new Order
                        {
                            OrderID = 1,
                            OrderPrice = 4.00,
                            StoreFront = store2,
                            Customer = new Customer
                            {
                                CustomerID = 1
                            }
                        },
                        new Order
                        {
                            OrderID = 2,
                            OrderPrice = 6.00,
                            StoreFront = store2,
                            Customer = new Customer
                            {
                                CustomerID = 2
                            }

                        },
                        new Order
                        {
                            OrderID = 3,
                            OrderPrice = 100.00,
                            StoreFront = store1,
                            Customer = new Customer
                            {
                                CustomerID = 3
                            }

                        }
                    );

                context.SaveChanges();
            }
        }
    }
}