using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using PaymentEngine.Stores;
using PaymentEngine.Terminals.Clients;
using Router;

using static PaymentEngine.Helpers.Serializer;

namespace PaymentEngine.UseCases.Payments.Callback {
    public class CallbackUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly RouterEngine _engine;
        private readonly TerminalStore _terminalStore;
        
        public CallbackUseCase(PaymentStore paymentStore, RouterEngine engine, TerminalStore terminalStore) {
            _paymentStore = paymentStore;
            _engine = engine;
            _terminalStore = terminalStore;
        }
        
        
         public static dynamic Convert(XElement parent)
        {
            dynamic output = new ExpandoObject();

            output.Name = parent.Name.LocalName;
            output.Value = parent.Value;

            output.HasAttributes = parent.HasAttributes;
            if (parent.HasAttributes)
            {
                output.Attributes = new List<KeyValuePair<string, string>>();
                foreach (XAttribute attr in parent.Attributes())
                {
                    KeyValuePair<string, string> temp = new KeyValuePair<string, string>(attr.Name.LocalName, attr.Value);
                    output.Attributes.Add(temp);
                }
            }

            output.HasElements = parent.HasElements;
            if (parent.HasElements)
            {
                output.Elements = new List<ExpandoObject>();
                foreach (XElement element in parent.Elements())
                {
                    dynamic temp = Convert(element);
                    output.Elements.Add(temp);
                }
            }

            return output;
        } 
         
        
        public async Task<CallbackResponse> ExecuteAsync(CallbackRequest request, CancellationToken token) {
            var allocations = _paymentStore.GetAllocationsByReference(request.Reference).ToList();
           
            if (!allocations.Any()) return new CallbackResponse();
            
           
            var req2 = new Request<string> {
                Name = "callback",
                RequestType = (int) RequestType.Callback,
                Payload = request.Data,
                Terminals = new Dictionary<string, int>{ { allocations.First().Terminal, 2 } }
            };

           
            var response =  await _engine.ProcessAsync(req2, token);
            var xml = response.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(xml)) return new CallbackResponse();

            var resp = DeSerialize<TerminalResponse>(xml);

            if (resp.IsSuccessfull && resp.Code == "00") 
                allocations.ForEach(a => a.AllocationStatusId = 6);
            
            return new CallbackResponse{ TerminalResponse = resp};
        }
    }
}