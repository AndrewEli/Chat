﻿using SharedClasses;
using SharedClasses.Domain;

namespace Server.MessageHandler
{
    /// <summary>
    /// Holds a reference to the necessary objects a <see cref="AvatarRequestHandler"/> needs. 
    /// </summary>
    internal sealed class AvatarRequestContext : IMessageContext
    {
        private readonly EntityIdAllocatorFactory entityIdAllocatorFactory;
        private readonly UserRepository userRepository;

        public AvatarRequestContext(UserRepository userRepository, EntityIdAllocatorFactory entityIdAllocatorFactory)
        {
            this.entityIdAllocatorFactory = entityIdAllocatorFactory;
            this.userRepository = userRepository;
        }

        public UserRepository UserRepository
        {
            get { return userRepository; }
        }

        public EntityIdAllocatorFactory EntityIdAllocatorFactory
        {
            get { return entityIdAllocatorFactory; }
        }
    }
}