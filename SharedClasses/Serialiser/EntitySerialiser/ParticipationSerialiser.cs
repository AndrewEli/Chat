﻿using System.Diagnostics.Contracts;
using System.Net.Sockets;
using log4net;
using SharedClasses.Domain;

namespace SharedClasses.Serialiser.EntitySerialiser
{
    /// <summary>
    /// Used to serialise and deserialise the <see cref="Participation" /> Domain object.
    /// </summary>
    internal sealed class ParticipationSerialiser
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ParticipationSerialiser));

        private readonly ISerialisationType serialiser = new BinarySerialiser();

        public void Serialise(NetworkStream networkStream, Participation participation)
        {
            Contract.Requires(participation != null);
            Contract.Requires(networkStream != null);

            serialiser.Serialise(networkStream, participation);
            Log.Debug("Participation serialised to the network stream");
        }

        public Participation Deserialise(NetworkStream networkStream)
        {
            Contract.Requires(networkStream != null);

            var participation = (Participation) serialiser.Deserialise(networkStream);
            Log.Debug("Deserialised a participation object from the network stream.");
            return participation;
        }
    }
}