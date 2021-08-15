using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router;
using Playground.Router.Clients;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.PaymentEngine.Terminals {
    public class StreamHandler: IStreamHandler {
        private static ReaderWriterLock _locker = new ();
        
        public Task<string> WriteAsync(Guid transactionId, Configuration configuration, string message, Terminal terminal, CancellationToken cancellationToken) {
            var request = DeSerialize<StreamRequest>(message);
            var filePath = configuration.Settings.First(s => s.Name == "file_path").Value;

            var parent = Directory.GetCurrentDirectory();
            var fileName =   $"{filePath}/{terminal.Name}_{transactionId}.csv";
            var path = $"{parent}/{fileName}";
            
            try
            {
                _locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
                WriteContent();
            }
            finally { _locker.ReleaseWriterLock(); }

            var response = new StreamResponse {
                Name = nameof(StreamHandler),
                Amount = request!.Amount,
                Code = request.Code,
                Reference = request.Reference,
                FileName = fileName
            };
            
            return Task.FromResult(Serialize(response));

             void WriteContent() {
                 var file = new FileInfo(path);
                 FileStream fs;

                 if (file.Exists) 
                     fs = file.Open(FileMode.Append, FileAccess.Write);
                 else {
                     fs = file.Open(FileMode.OpenOrCreate, FileAccess.Write);
                     WriteData(string.Join(",", request!.MetaData.Select(m => $"\"{m.Name}\"")));
                 }

                 WriteData(string.Join(",", request!.MetaData.Select(m => $"\"{m.Value}\"")));
                 
                 fs.Close();

                 void WriteData(string data) {
                     var bytes = Encoding.UTF8.GetBytes($"{data}\r\n");
                     fs.Write(bytes);
                 }
             }
        }
    }
}