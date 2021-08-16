using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.Core.Events;
using Playground.Router.Clients;

namespace Playground.Router.Old {
    internal class TerminalProcessor2<T> where T : class {
        private readonly Guid _transactionId;
        private readonly Request<T> _request;
        private readonly TerminalStore _store;
        private readonly List<string> _response = new();
        private readonly CancellationToken _cancellationToken;
        private readonly TransactionFactory<T> _transactionFactory;

        private TerminalProcessor2(
            EventHub eventHub,
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

            _transactionFactory = new TransactionFactory<T>(eventHub, request, factory, extensions);
        }

        public static async Task<List<string>> ProcessAsync<TS>(
            EventHub eventHub,
            Guid transactionId,
            Request<TS> request,
            TerminalStore store,
            TerminalExtensions extensions,
            ClientFactory factory,
            CancellationToken cancellationToken) where TS : class {
            var p = new TerminalProcessor2<TS>(eventHub, transactionId, request, store, extensions, factory, cancellationToken);
            return await p.ProcessAsync();
        }

        private async Task<List<string>> ProcessAsync() {
            var terminals = await GetTerminals(_request.Terminals);

            foreach (var t in terminals)
                if (await TryProcessAsync(t))
                    break;

            return _response;
        }
        
        private async Task<bool> TryProcessAsync(OldTerminal terminal) {
            for (var i = 0; i < terminal.RetryCount; i++) {
                if (await TryProcessRequestAsync(terminal))
                    return true;

                await Task.Delay(200, _cancellationToken);
            }

            return false;
        }
        
        private async Task<bool> TryProcessRequestAsync(OldTerminal terminal) {
            try {
                var (message, success) = await _transactionFactory.Create(_transactionId, terminal)
                                                                  .ProcessAsync(_cancellationToken);

                if (string.IsNullOrWhiteSpace(message))
                    return success;
                
                _response.Add(message);
                
                return success;
            }
            catch {
                return false;
            }
        }

        private async Task<IEnumerable<OldTerminal>> GetTerminals(IEnumerable<string> terminals) =>
            await _store.GetTerminalsAsync(terminals, _cancellationToken);
    }
}