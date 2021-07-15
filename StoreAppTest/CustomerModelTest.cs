using System;
using StoreAppModel;
using Xunit;

namespace StoreAppTest
{
    public class CustomerModelTest
    {
        /// <summary>
        /// This test will check if validation works in Customer Model
        /// It will input the right data and see if it persists
        /// </summary>
        [Fact]
        public void NameShouldSetValidData()
        {
            //Arrange
            Customer test = new Customer();
            string name = "Wing Lin";

            //Act
            test.Name = name;

            //Assert
            Assert.Equal(name, test.Name);
        }

        
    }
}
