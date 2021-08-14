using System.Xml.Linq;
using static System.Text.Json.JsonSerializer;
using static Newtonsoft.Json.JsonConvert;

namespace Playground.Xml {
    public static class ExtensionMethods {
        public static object? ToJson(this string xml) => 
            string.IsNullOrWhiteSpace(xml) ? null : Deserialize<object>(SerializeXNode(XDocument.Parse(xml)).Replace("@", ""));

        public static XElement ToXml(this string xml) => 
            (string.IsNullOrWhiteSpace(xml) ? null : XDocument.Parse(xml).Root) ?? new XElement("xml");
    }
}