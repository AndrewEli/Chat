﻿using System.Net.Sockets;
using SharedClasses.Message;

namespace SharedClasses.Serialiser.MessageSerialiser
{
    /// <summary>
    /// Used to Serialise and Deserialise a <see cref="ConversationSnapshot" /> object.
    /// </summary>
    internal sealed class ConversationSnapshotSerialiser : Serialiser<ConversationSnapshot>
    {
        private readonly BinarySerialiser binaryFormatter = new BinarySerialiser();

        protected override void Serialise(NetworkStream networkStream, ConversationSnapshot message)
        {
            binaryFormatter.Serialise(networkStream, message);
            Log.InfoFormat("{0} serialised and sent to network stream", message.MessageIdentifier);
        }

        public override IMessage Deserialise(NetworkStream networkStream)
        {
            var conversationSnapshot = (ConversationSnapshot) binaryFormatter.Deserialise(networkStream);
            Log.InfoFormat("Network stream has received data and deserialised to a {0} object", conversationSnapshot.MessageIdentifier);
            return conversationSnapshot;
        }
    }
}