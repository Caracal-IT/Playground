using System.IO;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.File {
    public abstract class FileStore {
        protected T GetRepository<T>() {
            var ns = GetType().Namespace!.Split('.');
            //var path = Path.Join(ns[^3], ns[^2], ns[^1], "repository.xml");
            var path = Path.Join("bin", "Debug", "net6.0", ns[^1], "repository.xml");
            
            using var fileStream = new FileStream(path, FileMode.Open);
            return (T) new XmlSerializer(typeof(T)).Deserialize(fileStream);
        }
    }
}