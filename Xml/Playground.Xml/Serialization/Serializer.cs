using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Playground.Xml.Serialization {
    public class Serializer {
        public static string Serialize<T>(T data) {
            var xml = new StringBuilder();
            var ns = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, string.Empty) });
            new XmlSerializer(typeof(T)).Serialize(new StringWriter(xml), data, ns);
            return xml.ToString();
        }
        
        public static T? DeSerialize<T>(string xml) where T : class, new() => 
            new XmlSerializer(typeof(T)).Deserialize(new StringReader(xml)) as T ?? new T();
    }
}