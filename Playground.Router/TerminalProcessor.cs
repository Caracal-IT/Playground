using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router.Clients;

namespace Playground.Router {
    internal class TerminalProcessor<T> where T : class {
        private readonly Guid _transactionId;
        private readonly Request<T> _request;
        private readonly TerminalStore _store;
        private readonly List<string> _response = new();
        private readonly CancellationToken _cancellationToken;
        private readonly TransactionFactory<T> _transactionFactory;

        private TerminalProcessor(
            Guid transactionId,
            Request<T> request,
            TerminalStore store,
            TerminalExtensions extensions,
            ClientFactory factory,
            CancellationToken cancellationToken) 
        {
            _store = store;
            _request = request;
            _transactionId = transactionId;
            _cancellationToken = cancellationToken;

            _transactionFactory = new TransactionFactory<T>(request, factory, extensions);
        }

        public static async Task<List<string>> ProcessAsync<TS>(
            Guid transactionId,
            Request<TS> request,
            TerminalStore store,
            TerminalExtensions extensions,
            ClientFactory factory,
            CancellationToken cancellationToken) where TS : class {
            var p = new TerminalProcessor<TS>(transactionId, request, store, extensions, factory, cancellationToken);
            return await p.ProcessAsync();
        }

        private async Task<List<string>> ProcessAsync() {
            var terminals = await GetTerminals(_request.Terminals);

            foreach (var t in terminals)
                if (await TryProcessAsync(t))
                    break;

            return _response;
        }
        
        private async Task<bool> TryProcessAsync(Terminal terminal) {
            for (var i = 0; i < terminal.RetryCount; i++) {
                if (await TryProcessRequestAsync(terminal))
                    return true;

                await Task.Delay(200, _cancellationToken);
            }

            return false;
        }
        
        private async Task<bool> TryProcessRequestAsync(Terminal terminal) {
            try {
                var response = await _transactionFactory.Create(_transactionId, terminal).ProcessAsync(_cancellationToken);

                if (string.IsNullOrWhiteSpace(response))
                    return false;
                
                _response.Add(response);
                
                return true;
            }
            catch {
                return false;
            }
        }

        private async Task<IEnumerable<Terminal>> GetTerminals(IEnumerable<string> terminals) =>
            await _store.GetTerminalsAsync(terminals, _cancellationToken);
    }
}