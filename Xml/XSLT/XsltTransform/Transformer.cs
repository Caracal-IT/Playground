using System.Collections.Generic;
using System.Text;

using XsltTransform.Extensions;

namespace XsltTransform {
    public class Transformer {
        private readonly string _xslt;
        private readonly string _xml;
        private readonly IDictionary<string, object> _args;
        
        private Transformer(string xml, string xslt, IDictionary<string, object>? args) {
            _xslt = xslt;
            _xml = xml;
            _args = args ?? new Dictionary<string, object>();
        }
        
        public static string Transform(string xml, string xslt, IDictionary<string, object>? extensions = null) => 
            new Transformer(xml, xslt, extensions).Transform();

        private string Transform() {
            var output = new StringBuilder();
            _xslt.XslTransform()
                 .Transform(_xml.Reader(), _args.Extensions(), output.Writer());
            
            return output.ToString();
        }
    }
}