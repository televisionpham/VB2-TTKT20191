using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VYT.ApplicationServiceClient
{
    public class WebApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        public WebApiClient(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<Models.Job> GetJob(int id)
        {
            var path = $"{_client.BaseAddress}/api/Job/{id}";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var job = await response.Content.ReadAsAsync<Models.Job>();
                return job;
            }
            return null;
        }
    }
}
