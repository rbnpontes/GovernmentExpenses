using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GovernmentExpenses.Core
{
    public sealed class Utils
    {
        public static T DeserializeFile<T>(string path)
        {
            T result = default(T);
            using (StreamReader reader = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                result = (T)serializer.Deserialize(reader,typeof(T));
            }
            return result;
        }
        public static void SerializeToFile<T>(string path, T obj)
        {
            using(StreamWriter writer = File.CreateText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, obj);
            }
        }
    }
}
