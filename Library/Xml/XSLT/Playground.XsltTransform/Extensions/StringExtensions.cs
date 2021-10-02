namespace Playground.XsltTransform.Extensions;

public static class StringExtensions {
    public static XslCompiledTransform XslTransform(this string xslt) {
        var transform = new XslCompiledTransform();
        transform.Load(xslt.Reader());
        return transform;
    }
    
    public static string Transform(this string xml, string xslt, IDictionary<string, object>? extensions = null) => 
        Transformer.Transform(xml, xslt, extensions);
    
    public static XmlTextReader Reader(this string text) =>
        new (new StringReader(text));

    public static XmlTextWriter Writer(this StringBuilder sb) =>
        new (new StringWriter(sb));
}