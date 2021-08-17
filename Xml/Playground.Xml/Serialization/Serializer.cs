using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Playground.Xml.Serialization {
    public static class Serializer {
        public static string Serialize<T>(T? data) =>
            data?.ToXml()??string.Empty;
            
        public static string ToXml(this object data) {
            var xml = new StringBuilder();
            var ns = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, string.Empty) });
            new XmlSerializer(data.GetType()).Serialize(new StringWriter(xml), data, ns);
            return XDocument.Parse(xml.ToString()).Root?.ToString()??"<xml/>";
        }

        public static XDocument ToXDocument(this string xml) =>
            XDocument.Parse(xml);
        
        public static T? DeSerialize<T>(string xml) where T : class, new() => 
            new XmlSerializer(typeof(T)).Deserialize(new StringReader(xml)) as T ?? new T();

        public static T ToObject<T>(this XNode node) where T : class, new() =>
            DeSerialize<T>(node.ToString())??new T();
    }
}