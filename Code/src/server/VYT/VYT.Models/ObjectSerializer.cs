using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace VYT.Models
{
    public class ObjectSerializer
    {
        public static void JSONSerializeToFile(object data, string filePath)
        {
            var serializer = new JsonSerializer();
            serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            var folder = Path.GetDirectoryName(filePath);
            if (folder != null && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            using (var sw = new StreamWriter(filePath))
            {
                using (var writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, data);
                }
            }
        }

        public static T JSONDeserilizeFromFile<T>(string filePath)
        {
            var serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.Auto;
            using (var sr = new StreamReader(filePath))
            {
                using (var reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<T>(reader);
                }
            }
        }

        public static string JSONSerializeObjectToString(object data)
        {
            var ret = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return ret;
        }

        public static T JSONDeserializeFromString<T>(string json)
        {
            var ret = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            return ret;
        }

        public static T XmlDeserializeFromFile<T>(string file)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException("File not found", file);
            }
            var ds = new DataContractSerializer(typeof(T));
            using (var s = File.OpenRead(file))
            {
                var data = ds.ReadObject(s);
                return (T)data;
            }
        }
    }
}