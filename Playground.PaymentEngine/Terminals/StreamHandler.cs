using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router;
using Playground.Router.Clients;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.PaymentEngine.Terminals {
    public class StreamHandler: IStreamHandler {
        public async Task<string> WriteAsync(Guid transactionId, Configuration configuration, string message, Terminal terminal, CancellationToken cancellationToken) {
            var request = DeSerialize<StreamRequest>(message);
            var filePath = configuration.Settings.First(s => s.Name == "file_path").Value;

            var parent = Directory.GetCurrentDirectory();
            var fileName =   $"{filePath}/test.txt";
            var path = $"{parent}/{fileName}";

            await using var writer = File.AppendText(path);
            await writer.WriteLineAsync($"{transactionId}");
            await writer.WriteLineAsync(message);
            await writer.WriteLineAsync("\r\n");

            await Task.Delay(1, cancellationToken);
            
            var response = new StreamResponse {
                Name = nameof(StreamHandler),
                Amount = request!.Amount,
                Code = request.Code,
                Reference = request.Reference,
                FileName = fileName
            };
            
            return Serialize(response);
        }
    }
}