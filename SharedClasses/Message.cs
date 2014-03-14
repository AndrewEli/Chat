﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using SharedClasses.Serialisation;

namespace SharedClasses
{
    [Serializable]
    public class Message
    {
        private static readonly log4net.ILog Log =
                log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

       private static readonly ITcpSendBehaviour SerialiseMessage = new BinaryFormat();

        public string text { get; private set; }
        public DateTime messageTimeStamp;

        public Message(string text)
        {
            CreateMessage(text);
            Log.Info("Message created");
        }

        private void CreateMessage(string messageText)
        {
            SetTextOfMessage(messageText);

            if (text != null)
            {
                SetTimeStampOfMessage();
            }
        }

        public void Serialise(Stream newtworkStream)
        {
            SerialiseMessage.Serialise(newtworkStream, this);
    
        }

        public static Message Deserialise(Stream newtworkStream)
        {
            return SerialiseMessage.Deserialise(newtworkStream);
        }

        private void SetTextOfMessage(string messageText)
        {
            text = messageText;
            Log.Debug("Message text set: " + text);
        }

        private void SetTimeStampOfMessage()
        {
            messageTimeStamp = DateTime.Now;
            Log.Debug("Time stamp created: " + messageTimeStamp);
        }
    }
}