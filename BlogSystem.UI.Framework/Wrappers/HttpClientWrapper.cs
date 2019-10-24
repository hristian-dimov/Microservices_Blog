using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.UI.Framework.Wrappers
{
    public static class HttpClientWrapper
    {
        public static async Task<string> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                var result = await content.ReadAsStringAsync();

                return result;
            }
        }

        public static async Task<string> PostAsync(string url, object input)
        {
            using (StringContent requestContent = new StringContent(ConvertToJsonString(input), Encoding.UTF8, "application/json"))
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.PostAsync(url, requestContent))
            using (HttpContent content = response.Content)
            {
                var result = await content.ReadAsStringAsync();

                return result;
            }
        }

        private static string ConvertToJsonString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }

            if (obj is string stringObject)
            {
                return stringObject;
            }

            string serializedObject = JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return serializedObject;
        }
    }
}