namespace Playground.Xml.Serialization;

public static class Serializer {
    public static string Serialize<T>(T data) =>
        data?.ToXml() ?? string.Empty;

    public static T DeSerialize<T>(string xml) where T : class, new() =>
        new XmlSerializer(typeof(T)).Deserialize(new StringReader(xml)) as T ?? new T();
}