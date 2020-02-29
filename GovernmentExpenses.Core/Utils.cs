using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace GovernmentExpenses.Core
{
    public static class Utils
    {
        public static string RemoveDiacritics(this string str)
        {
            return string.Concat(str.Normalize(NormalizationForm.FormD).Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)).Normalize(NormalizationForm.FormC);
        }
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
