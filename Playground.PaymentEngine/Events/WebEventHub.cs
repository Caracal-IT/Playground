using Microsoft.Extensions.Logging;
using Playground.Core.Events;

namespace Playground.PaymentEngine.Events {
    public class WebEventHub: EventHub {
        private ILogger<EventHub> _logger;

        public WebEventHub(ILogger<EventHub> logger) => 
            _logger = logger;

        public void Publish(string message) => 
            _logger.LogInformation(message);
    }
}