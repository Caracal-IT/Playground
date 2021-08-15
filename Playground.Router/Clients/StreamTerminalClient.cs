using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router.Clients {
    public class StreamTerminalClient: Client {
        private readonly IStreamHandler _streamHandler;

        public StreamTerminalClient(IStreamHandler streamHandler) {
            _streamHandler = streamHandler;
        }
        
        public async Task<string> SendAsync(Guid transactionId, Configuration configuration, string message, Terminal terminal, CancellationToken cancellationToken) => 
            await _streamHandler.WriteAsync(transactionId, configuration, message, terminal, cancellationToken);
    }
}