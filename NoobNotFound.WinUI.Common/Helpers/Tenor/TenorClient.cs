using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace NoobSharp.Common.WinUI.Helpers.Tenor
{
    public class TenorClient : IDisposable
    {
        public HttpClient Client;
        public string APIKey { get; set; }
        public string ClientKey { get; set; }
        public TenorClient(string apiKey,string clientKey = null)
        {
            APIKey = apiKey;
            ClientKey = clientKey;
            Client = new HttpClient
            {
                BaseAddress = new Uri("https://tenor.googleapis.com/v2/")
            };
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private async Task<string> Get(string code)
        {
            try
            {
                HttpResponseMessage response = await Client.GetAsync(code);
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception("Failed to get: " + code);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get: " + code, ex);
            }
        }
        public async Task RegisterShare(string id,string query) =>
            await Get(ToClientKeyUrl($"registershare?id={id}&key={APIKey}&q={query}"));
        
        private string ToClientKeyUrl(string d) =>
            string.IsNullOrEmpty(ClientKey) ? d : d + $"&client_key={ClientKey}";

        public async Task<JSON.SearchResult.Root> Search(string query, int limit = 12)
        {
            var q = HttpUtility.UrlEncode(query);
            try
            {
                var r = await Get(ToClientKeyUrl($"search?q={q}&key={APIKey}&limit={limit}"));
                var d = JsonConvert.DeserializeObject<JSON.SearchResult.Root>(r);
                return d;
            }
            catch
            {
                return null;
            }
        }
        public async Task<JSON.SearchResult.Root> GetTrendings()
        {
            try
            {
                var r = await Get(ToClientKeyUrl($"featured?key={APIKey}"));
                var d = JsonConvert.DeserializeObject<JSON.SearchResult.Root>(r);
                return d;
            }
            catch
            {
                return null;
            }
        }

        public void Dispose()=>
            Client.Dispose();
        
    }
}
