using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GovernmentExpenses.Core
{
    public sealed class Utils
    {
        public static float ParseCurrency(string value)
        {
            float result = 0;
            float.TryParse(value.Replace(",", "."), out result);
            return result;
        }
        public static T DeserializeFile<T>(string path)
        {
            T result = default(T);
            using (StreamReader stream = File.OpenText(path))
            {
                using (JsonTextReader reader = new JsonTextReader(stream))
                {
                    reader.SupportMultipleContent = true;
                    JsonSerializer serializer = new JsonSerializer();
                    result = serializer.Deserialize<T>(reader);
                }
            }
            return result;
        }
        public static void SerializeToFile<T>(string path, T obj)
        {
            using (StreamWriter writer = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }
        }
    }
}
