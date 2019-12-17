
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RagnarockApp.UserNicolai.Model;

namespace UnitTest123
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange

            //Act

            //Assert
            Assert.AreEqual(1,1);

        }
        [TestMethod]
        public void TestOpretBruger()
        {
            //Arrange
            UserCatalogSingleton userCatalog = UserCatalogSingleton.UserInstants;
            //Act
            int numberOfUsersBefore = userCatalog.Users.Count;
            User userOne = new User("poul", 123456, true, "PMan","1234567");
            userCatalog.AddUser(userOne);
            int numberOfUsersAfter = userCatalog.Users.Count;
            //Assert
            Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);

        }

        [TestMethod]
        public void TestSletBruger()
        {
            //Arrange
            UserCatalogSingleton userCatalog2 = UserCatalogSingleton.UserInstants;
            //Act
            int numberOfUsersBefore1 = userCatalog2.Users.Count;
            User userTwo = new User("Pia", 123456, true, "PGirl", "12345678");
            userCatalog2.RemoveAt(0);
            int numberOfUserAfter1 = userCatalog2.Users.Count;
            //Assert
            Assert.AreEqual(numberOfUsersBefore1 -1, numberOfUserAfter1);

        }
    }
}
