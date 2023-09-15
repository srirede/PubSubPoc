using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace QueuingService
{
    public interface IPubSubQueuing
    {
        Task<string> SendMessage(PubsubMessage pubsubMessage);
    }
    public class PubSubQueuing : IPubSubQueuing
    {
        private readonly PublisherClient _publisherClient;
        private readonly ILogger<PubSubQueuing> _logger;

        public PubSubQueuing(PublisherClient publisherClient, ILogger<PubSubQueuing> logger)
        {
            _publisherClient = publisherClient;
            _logger = logger;
        }

        public async Task<string> SendMessage(PubsubMessage pubsubMessage)
        {
            _logger.Log(LogLevel.Information, JsonConvert.SerializeObject(pubsubMessage));
            return await _publisherClient.PublishAsync(pubsubMessage);
        }
    }
}
