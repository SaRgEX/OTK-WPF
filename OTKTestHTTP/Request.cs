using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Pipes;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

namespace OTKLib
{
    public class Request
    {
        public static async Task<dynamic> GetJsonArrayFromHttpServer(string url)
        {
            using var client = new HttpClient();

            var result = await client.GetStringAsync(url);
            dynamic json = JsonConvert.DeserializeObject(result);
            if (json == null)
                return null;
            return json;
            
        }

        public static async Task<bool> PostRequest(string url, object data)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync($"{url}", data);              
                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> PutRequest(string url, int article, HttpContent data)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync($"{url}/{article}", data);
                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<bool> DeleteRequest(string url, int article)
        {
            using (var client = new HttpClient())
            {
                var response = await client.DeleteAsync($"{url}/{article}");
                return response.IsSuccessStatusCode;
            }
        }
    }
}