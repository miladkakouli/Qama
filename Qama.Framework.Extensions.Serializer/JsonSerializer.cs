using System.Text;
using Newtonsoft.Json;

namespace Qama.Framework.Extensions.Serializer
{
    public static class JsonSerializer
    {
        public static byte[] ToJsonByteArray(this object obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T FromJsonByteArray<T>(this byte[] byteArray) where T : class
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(byteArray));
        }

        public static T FromJsonString<T>(this string obj)
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
