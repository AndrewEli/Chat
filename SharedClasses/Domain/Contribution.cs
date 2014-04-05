﻿using System;
using log4net;
using SharedClasses.Protocol;

namespace SharedClasses.Domain
{
    [Serializable]
    public class Contribution : IMessage
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Contribution));

        private DateTime messageTimeStamp;
        private string text;

        public Contribution(string text)
        {
            CreateMessage(text);
            Identifier = SerialiserRegistry.IdentifiersByMessageType[typeof(Contribution)];
            Log.Debug("Contribution created");
        }

        public string GetMessage()
        {
            return text + " @ " + messageTimeStamp;
        }

        private void CreateMessage(string messageText)
        {
            SetTextOfMessage(messageText ?? String.Empty);
            SetTimeStampOfMessage();
        }

        private void SetTextOfMessage(string messageText)
        {
            text = messageText;
            Log.Debug("Contribution text set: " + text);
        }

        private void SetTimeStampOfMessage()
        {
            messageTimeStamp = DateTime.Now;
            Log.Debug("Time stamp created: " + messageTimeStamp);
        }

        public int Identifier { get; private set; }
    }
}