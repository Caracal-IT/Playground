using System.IO;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.File {
    public abstract class FileStore {
        protected T GetRepository<T>() {
            var ns = GetType().Namespace!.Split('.');
            var location = System.Reflection.Assembly.GetEntryAssembly()!.Location.Split(Path.PathSeparator);
            var path = Path.Join(location[^4], location[^3], location[^2], ns[^1], "repository.xml");
        
            using var fileStream = new FileStream(path, FileMode.Open);
            return (T) new XmlSerializer(typeof(T)).Deserialize(fileStream)!;
        }
    }
}