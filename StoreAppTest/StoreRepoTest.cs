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

        /// <summary>
        /// Initialize a test object. Database stored in a separate file from main application db
        /// and data is seeded to have controlled tests.
        /// </summary>
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
        /// This retrieves all StoreFront orders based on StoreFrontID (2)
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
        /// This should get a list of products that correlate to a specific StoreID
        /// </summary>
        [Fact]
        public void GetStoreProductsShouldReturnStoreProducts()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                List<Product> products;
                IRepository repo = new Repository(context);

                products = repo.GetStoreProducts(1);

                Assert.NotNull(products);
                Assert.Equal(2, products.Count);
            }
        }

        /// <summary>
        /// This should show that UpdateInventory persists in the database for the Products table
        /// </summary>
        [Fact]
        public void UpdateInventoryShouldPersist()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                Product product1, product2;
                IRepository repo = new Repository(context);
                product1 = repo.GetProductByID(1);   // starts with quantity (10)
                product2 = repo.GetProductByID(2);   // starts with quantity (11)
                
                repo.UpdateInventory(product1.ProductID, 100);
                repo.UpdateInventory(product2.ProductID, -5);
                
                product1 = repo.GetProductByID(1);
                product2 = repo.GetProductByID(2);
                Assert.Equal(110, product1.ProductQuantity);
                Assert.Equal(6, product2.ProductQuantity);

            }
        }

        /// <summary>
        /// This should return a List of Product objects that make up a store's inventory.
        /// </summary>
        [Fact]
        public void GetStoreInventoryShouldReturnStoreProducts()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Product> inventory;

                inventory = repo.GetStoreProducts(1);
                
                Assert.NotNull(inventory);
                Assert.Equal(2, inventory.Count);

            }
        }

        /// <summary>
        /// This should return a list of Product IDs from a specific store.
        /// </summary>
        [Fact]
        public void GetStoreProductIDsShouldReturnStoreProductIDs()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<int> productIDs;

                productIDs = repo.GetStoreProductIDs(1);

                Assert.Equal(2, productIDs.Count);
            }
        }

        /// <summary>
        /// This should return a List of all products from the Products table.
        /// </summary>
        [Fact]
        public void GetAllStoreInventoryShouldReturnAllProducts()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                List<Product> products;

                products = repo.GetAllStoreInventory();

                Assert.Equal(3, products.Count);
            }
        }

        /// <summary>
        /// This should return the Address property of a specific StoreFront.
        /// </summary>
        [Fact]
        public void GetStoreLocationShouldReturnStoreAddress()
        {
            using (var context = new StoreAppDBContext(_options))
            {
                IRepository repo = new Repository(context);
                string location = "";

                location = repo.GetStoreLocation(1);

                Assert.Equal("321 Lego Avenue", location);
            }
        }

        /// <summary>
        /// This seeds (2) StoreFront, (2) Products, and (3) Order data entries into a database (should be clean).
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
                store1.StoreFrontID = 1;
                context.Products.AddRange(
                    new Product
                    {
                        ProductName = "Toy truck",
                        ProductPrice = 5.00,
                        ProductQuantity = 10,
                        StoreFront = store1
                    },
                    new Product
                    {
                        ProductName = "Toy boat",
                        ProductPrice = 6.00,
                        ProductQuantity = 11,
                        StoreFront = store1
                    },
                    new Product
                    {
                        ProductName = "Toy car",
                        ProductPrice = 2.00,
                        ProductQuantity = 40,
                        StoreFront = store2
                    }
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