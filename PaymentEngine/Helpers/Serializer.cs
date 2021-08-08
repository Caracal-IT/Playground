using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PaymentEngine.Helpers {
    public static class Serializer {
        public static string Serialize<T>(T data) {
            var xml = new StringBuilder();
            var ns = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, string.Empty) });
            new XmlSerializer(typeof(T)).Serialize(new StringWriter(xml), data, ns);
            return xml.ToString();
        }
    }
}