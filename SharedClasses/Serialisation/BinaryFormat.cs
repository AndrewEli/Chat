﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using log4net;

namespace SharedClasses.Serialisation
{
    public class BinaryFormat : ITcpSendBehaviour
    {
        private static readonly ILog Log =
            LogManager.GetLogger(typeof (BinaryFormat));

        public void Serialise(NetworkStream networkStream, Message clientMessage)
        {
            var binaryFormatter = new BinaryFormatter();
            try
            {
                if (networkStream.CanWrite)
                {
                    Log.Info("Attempt to serialise message and send to stream");
                    binaryFormatter.Serialize(networkStream, clientMessage);
                    Log.Info("Message serialised and sent to network stream");
                }
            }
            catch (IOException ioException)
            {
                Log.Error("connection lost between the client and the server", ioException);
            }
        }

        public Message Deserialise(NetworkStream networkStream)
        {
            var binaryFormatter = new BinaryFormatter();
            try
            {
                if (networkStream.CanRead)
                {

                    Log.Debug("Network stream can be read from, waiting for message");
                    var message = (Message) binaryFormatter.Deserialize(networkStream);
                    Log.Info("Network stream has received data and deserialised to a message object");
                    return message;
                }
            }
            catch (IOException ioException)
            {
                Log.Error("connection lost between the client and the server", ioException);
                networkStream.Close();
            }
            finally
            {
            }
            return new Message(string.Empty);
        }
    }
}