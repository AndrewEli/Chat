﻿using System.Net.Sockets;
using log4net;
using SharedClasses.Domain;

namespace SharedClasses.Protocol
{
    public class ContributionRequestSerialiser : ISerialiser<ContributionRequest>
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (ContributionRequestSerialiser));

        private readonly ContributionSerialiser serialiser = new ContributionSerialiser();

        public void Serialise(ContributionRequest contributionRequest, NetworkStream stream)
        {
            MessageUtilities.SerialiseMessageIdentifier(contributionRequest.GetMessageIdentifier(), stream);
            var message = contributionRequest;

            Log.Debug("Waiting for contribution request message to serialise");
            serialiser.Serialise(message.Contribution, stream);
            Log.Info("Contribution request message serialised");
        }

        public void Serialise(IMessage contributionRequestMessage, NetworkStream stream)
        {
            Serialise((ContributionRequest) contributionRequestMessage, stream);
        }

        public IMessage Deserialise(NetworkStream stream)
        {
            Log.Debug("Waiting for a contribution request message to deserialise");
            var request = new ContributionRequest((Contribution)serialiser.Deserialise(stream));
            Log.Info("Contribution request message deserialised");
            return request;
        }
    }
}