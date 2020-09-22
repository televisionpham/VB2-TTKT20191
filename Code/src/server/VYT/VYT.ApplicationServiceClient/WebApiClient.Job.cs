using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VYT.ApplicationServiceClient
{
    public partial class WebApiClient
    {
        private readonly HttpClient _client = new HttpClient();
        public WebApiClient(string baseAddress)
        {
            _client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<int> GetTotalJobs()
        {
            var path = $"{_client.BaseAddress}/api/Job/Total";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<int>();
                return result;
            }
            else
            {
                return 0;
            }
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

        public async Task<IEnumerable<Models.Job>> GetJobPage(int pageIndex, int pageSize)
        {
            var path = $"{_client.BaseAddress}/api/Job?pageIndex={pageIndex}&pageSize={pageSize}";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<Models.Job>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Models.JobFile>> GetJobFiles(int id, int type)
        {
            var path = $"{_client.BaseAddress}/api/Job/GetFiles?id={id}&type={type}";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<Models.JobFile>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdateJob(Models.Job job)
        {
            var path = $"{_client.BaseAddress}/api/Job";
            var resp = await _client.PutAsJsonAsync(path, job);
            return resp;
        }

        public async Task<HttpResponseMessage> RemoveJob(int id)
        {
            var path = $"{_client.BaseAddress}/api/Job/{id}";
            var resp = await _client.DeleteAsync(path);
            return resp;
        }

        public async Task<Models.Job> CreateJob(string filePath, string languages)
        {
            var path = $"{_client.BaseAddress}/api/Job/Create";
            var multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(languages), "Languages");
            using (var fs = File.OpenRead(filePath))
            {
                multiForm.Add(new StreamContent(fs), "File", Path.GetFileName(filePath));
                var resp = await _client.PostAsync(path, multiForm);
                if (resp.IsSuccessStatusCode)
                {
                    var result = await resp.Content.ReadAsAsync<Models.Job>();
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
