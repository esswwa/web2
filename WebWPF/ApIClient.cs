using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebWPF
{
    public class ApiClient
    {
        private readonly string _baseAddress = "http://localhost:80";

        public async Task<T> GetAsync<T>(string relativeUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);

                HttpResponseMessage response = await client.GetAsync(relativeUrl);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(responseBody);
                return result;
            }
        }
    }
}
