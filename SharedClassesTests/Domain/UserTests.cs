﻿using System;
using NUnit.Framework;
using SharedClasses;
using SharedClasses.Domain;

namespace SharedClassesTests.Domain
{
    [TestFixture]
    public class UserTests
    {
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(8)]
        public void CustomUserIDTest(int id)
        {
            var user = new User("user", id);
            Assert.AreEqual(user.ID, id);
        }

        [Test]
        public void NoUniqueUserTest()
        {
            var userFactory = new UserFactory();
            User user1 = userFactory.CreateUser("User1");

            Assert.AreEqual(user1.ID, 0);
            User user2 = userFactory.CreateUser("User2");
            Assert.AreNotSame(user1.ID, user2.ID);
        }

        [Test]
        public void UserIDIterationTest()
        {
            var userFactory = new UserFactory();
            User user = null;

            for (int i = 0; i <= 100; i++)
            {
                user = userFactory.CreateUser("user");
            }

            if (user != null)
            {
                Assert.AreEqual(user.ID, 100);
            }
        }

        [Test]
        public void UserIDLowerThanZeroTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new User("user", -4));
        }

        [Test]
        public void UsersWithSameNameAndSameIDEqualityTest()
        {
            var user1 = new User("User", 2);
            var user2 = new User("User", 2);
            Assert.AreEqual(user1, user2);
        }

        [Test]
        public void UsersWithSameNameHaveDifferentIDsEqualityTest()
        {
            var user1 = new User("User", 1);
            var user2 = new User("User", 2);
            Assert.AreNotEqual(user1, user2);
        }
    }
}