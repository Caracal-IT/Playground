using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using static System.Text.Json.JsonSerializer;
using static Newtonsoft.Json.JsonConvert;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.Xml {
    public static class ExtensionMethods {
        public static object? ToJson(this string xml) => 
            string.IsNullOrWhiteSpace(xml) ? null : Deserialize<object>(SerializeXNode(XDocument.Parse(xml)).Replace("@", string.Empty));
        
        public static string ToXml(this string json, string root) => 
            DeserializeXNode(json, root).ToString(SaveOptions.DisableFormatting);
        
        public static string ToXml(this object data) {
            var xml = new StringBuilder();
            var ns = new XmlSerializerNamespaces(new XmlQualifiedName[] { new XmlQualifiedName(string.Empty, string.Empty) });
            new XmlSerializer(data.GetType()).Serialize(new StringWriter(xml), data, ns);
            return XDocument.Parse(xml.ToString()).Root?.ToString()??"<xml/>";
        }

        public static XDocument ToXDocument(this string xml) =>
            XDocument.Parse(xml);

        public static T ToObject<T>(this XNode node) where T : class, new() =>
            DeSerialize<T>(node.ToString())??new T();
    }
}