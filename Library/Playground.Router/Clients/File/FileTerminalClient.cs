namespace Playground.Router.Clients.File;

using static System.Text.Encoding;
using static Xml.Serialization.Serializer;

public class FileTerminalClient : Client {
    private static readonly ReaderWriterLock Locker = new();

    public Task<string> SendAsync(Configuration configuration, string message, CancellationToken cancellationToken) {
        var request = DeSerialize<Request>(message);
        var filePath = configuration.Settings.First(s => s.Name == "file_path").Value;
        var terminal = configuration.Settings.First(s => s.Name == "Terminal").Value;
        var transactionId = configuration.Settings.First(s => s.Name == "TransactionId").Value;

        var parent = Directory.GetCurrentDirectory();
        var fileName = $"{filePath}/{terminal}_{transactionId}.csv";
        var path = $"{parent}/{fileName}";

        try {
            Locker.AcquireWriterLock(int.MaxValue);
            WriteContent();
        }
        finally {
            Locker.ReleaseWriterLock();
        }

        var response = new Response {
            Name = "Response_File",
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
                var bytes = UTF8.GetBytes($"{data}\r\n");
                fs.Write(bytes);
            }
        }
    }
}