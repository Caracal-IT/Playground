using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace Playground.XsltTransform.Extensions {
    public static class StringExtensions {
        public static XslCompiledTransform XslTransform(this string xslt) {
            var transform = new XslCompiledTransform();
            transform.Load(xslt.Reader());
            return transform;
        }
        public static XmlTextReader Reader(this string text) =>
            new XmlTextReader(new StringReader(text));

        public static XmlTextWriter Writer(this StringBuilder sb) =>
            new XmlTextWriter(new StringWriter(sb));
    }
}