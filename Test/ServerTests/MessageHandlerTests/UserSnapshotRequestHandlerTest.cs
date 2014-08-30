﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Server.MessageHandler;
using SharedClasses;
using SharedClasses.Domain;
using SharedClasses.Message;

namespace ServerTests.MessageHandlerTests
{
    [TestFixture]
    public class UserSnapshotRequestHandlerTest : MessageHandlerTestFixture
    {
        public override void BeforeEachTest()
        {
            base.BeforeEachTest();
            userSnapshotRequest = new UserSnapshotRequest(DefaultUser.Id);
        }

        private readonly UserSnapshotRequestHandler userSnapshotRequestHandler = new UserSnapshotRequestHandler();

        private UserSnapshotRequest userSnapshotRequest;

        public override void HandleMessage(IMessage message)
        {
            userSnapshotRequestHandler.HandleMessage(message, ServiceRegistry);
        }

        [TestFixture]
        public class HandleMessageTest : UserSnapshotRequestHandlerTest
        {
            [Test]
            public void SendsAMessage()
            {
                bool isMessageSent = false;
                ConnectedUserClientHandler.MessageSent += (sender, eventArgs) => isMessageSent = true;

                HandleMessage(userSnapshotRequest);

                Assert.IsTrue(isMessageSent);
            }


            [Test]
            public void SendsAUserSnapshotMessage()
            {
                IMessage message = null;

                ConnectedUserClientHandler.MessageSent += (sender, eventArgs) => message = eventArgs.Message;

                HandleMessage(userSnapshotRequest);

                Assert.IsTrue(message.MessageIdentifier == MessageIdentifier.UserSnapshot);
            }

            [Test]
            public void ThrowsExceptionWhenMessageIsNotUserSnapshotRequest()
            {
                var participationSnapshotRequest = new ConversationSnapshotRequest(DefaultUser.Id);
                Assert.Throws<InvalidCastException>(() => HandleMessage(participationSnapshotRequest));
            }

            [Test]
            public void UserSnapshotSentContainsAllUsers()
            {
                IMessage message = null;

                ConnectedUserClientHandler.MessageSent += (sender, eventArgs) => message = eventArgs.Message;

                HandleMessage(userSnapshotRequest);

                var userSnapshot = (UserSnapshot) message;

                IEnumerable<User> allUsers = ServiceRegistry.GetService<RepositoryManager>().GetRepository<User>().GetAllEntities();

                Assert.AreEqual(userSnapshot.Users, allUsers);
            }
        }
    }
}