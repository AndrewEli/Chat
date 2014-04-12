﻿using System;

namespace SharedClasses.Protocol
{
    /// <summary>
    ///     This class contains a username packaged as a request a client sends to log in to a server
    /// </summary>
    [Serializable]
    public class LoginRequest : IMessage
    {
        public LoginRequest(string userName)
        {
            UserName = userName;
            Identifier = MessageNumber.LoginRequest;
        }

        public string UserName { get; private set; }

        public int Identifier { get; private set; }
    }
}