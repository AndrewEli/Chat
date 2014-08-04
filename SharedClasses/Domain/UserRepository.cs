﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SharedClasses.Domain
{
    /// <summary>
    /// Holds a collection of <see cref="User"/>s with basic CRUD operations.
    /// </summary>
    public sealed class UserRepository : Repository<User>
    {
        /// <summary>
        /// Updates a <see cref="User"/>'s <see cref="ConnectionStatus"/>
        /// </summary>
        /// <param name="connectionStatus">The new connection status of the user.</param>
        public void UpdateUserConnectionStatus(ConnectionStatus connectionStatus)
        {
            Contract.Requires(connectionStatus != null);

            User user = FindEntityById(connectionStatus.UserId);

            User previousUser = User.DeepClone(user);

            user.ConnectionStatus = connectionStatus;

            var userChangedEventArgs = new EntityChangedEventArgs<User>();

            userChangedEventArgs.EntityUpdated(user, previousUser);

            OnEntityChanged(userChangedEventArgs);
        }

        /// <summary>
        /// Updates a <see cref="User"/>'s <see cref="Avatar"/>.
        /// </summary>
        /// <param name="avatar">The new avatar to give a user.</param>
        public void UpdateUserAvatar(Avatar avatar)
        {
            Contract.Requires(avatar != null);
            User user = FindEntityById(avatar.UserId);

            User previousUser = User.DeepClone(user);

            user.Avatar = avatar;

            var userChangedEventArgs = new EntityChangedEventArgs<User>();
            userChangedEventArgs.EntityUpdated(user, previousUser);

            OnEntityChanged(userChangedEventArgs);
        }

        /// <summary>
        /// Gets a <see cref="User"/> entity by username.
        /// </summary>
        /// <param name="username">The username that is used to find the <see cref="User"/>.</param>
        /// <returns>The <see cref="User"/> that matches the username.</returns>
        public User FindUserByUsername(string username)
        {
            return GetAllEntities().Where(user => user.Username == username).Select(user => user).FirstOrDefault();
        }
    }
}