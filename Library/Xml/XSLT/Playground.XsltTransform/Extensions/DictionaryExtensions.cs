using System.Collections.Generic;
using System.Xml.Xsl;

namespace Playground.XsltTransform.Extensions {
    public static class DictionaryExtensions {
        public static XsltArgumentList Extensions(this IDictionary<string, object> extensions) {
            var args = new XsltArgumentList();
            
            foreach (var (key, value) in extensions)
                args.AddExtensionObject(key, value);

            return args;
        }
    }
}