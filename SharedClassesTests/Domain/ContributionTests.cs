﻿using NUnit.Framework;
using SharedClasses.Domain;

namespace SharedClassesTests.Domain
{
    [TestFixture]
    public class ContributionTests
    {
        [Test]
        public void CompleteContributionTest()
        {
            const string Message = "Hello";

            var finalContribution = new Contribution(1, new Contribution(1, Message, 1));
            Assert.AreEqual(finalContribution.Message, Message);
        }

        [Test]
        public void EqualsTest()
        {
            const string Message = "Hello";

            var contribution = new Contribution(1, new Contribution(1, Message, 1));
            var contribution2 = new Contribution(1, new Contribution(1, Message, 1));

            Assert.AreEqual(contribution, contribution2);
            Assert.IsTrue(contribution.Equals(contribution2 as object));
        }

        [Test]
        public void GetContributorUserIdTest()
        {
            const string Message = "Hello";
            const int ContributorUserId = 2;

            var contribution = new Contribution(ContributorUserId, Message, 1);

            Assert.AreEqual(contribution.ContributorUserId, ContributorUserId);
        }

        [Test]
        public void GetDateTimeTest()
        {
            const string Message = "Hello";

            var finalContribution = new Contribution(1, new Contribution(1, Message, 1));
            Assert.IsNotNull(finalContribution.MessageTimeStamp);
        }

        [Test]
        public void HashcodeTest()
        {
            const string Message = "Hello";

            var contribution = new Contribution(1, new Contribution(1, Message, 1));
            var contribution2 = new Contribution(1, new Contribution(1, Message, 1));

            Assert.AreEqual(contribution.GetHashCode(), contribution2.GetHashCode());
        }

        [Test]
        public void IncompleteContributionTest()
        {
            const string Message = "Hello";
            var contribution = new Contribution(1, Message, 1);
            Assert.AreEqual(contribution.Message, Message);
        }

        [Test]
        public void ReferenceEqualsTest()
        {
            const string Message = "Hello";

            var contribution = new Contribution(1, new Contribution(1, Message, 1));

            Contribution contribution2 = contribution;

            Assert.IsTrue(contribution.Equals(contribution2));
            Assert.IsTrue(contribution.Equals(contribution2 as object));
            Assert.IsFalse(contribution.Equals(null));

            object contributionObject = contribution;

            Assert.IsFalse(contributionObject.Equals(2));
            Assert.IsFalse(contributionObject.Equals(null));
        }
    }
}