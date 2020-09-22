using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYT.Models;

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
            var resp = await _client.GetAsync(path);
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsAsync<int>();
                return result;
            }
            else
            {
                throw new Exception($"{resp.ReasonPhrase}");
            }
        }

        public async Task<Models.Job> GetJob(int id)
        {
            var path = $"{_client.BaseAddress}/api/Job/{id}";
            var resp = await _client.GetAsync(path);
            if (resp.IsSuccessStatusCode)
            {
                var job = await resp.Content.ReadAsAsync<Models.Job>();
                return job;
            }
            else
            {
                throw new Exception($"{resp.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Models.Job>> GetJobPage(int pageIndex, int pageSize)
        {
            var path = $"{_client.BaseAddress}/api/Job?pageIndex={pageIndex}&pageSize={pageSize}";
            var resp = await _client.GetAsync(path);
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsAsync<IEnumerable<Models.Job>>();
                return result;
            }
            else
            {
                throw new Exception($"{resp.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Models.Job>> GetJobByState(JobStateEnum state, int limit)
        {
            var path = $"{_client.BaseAddress}/api/Job/GetByState?state={(int)state}&limit={limit}";
            var resp = await _client.GetAsync(path);
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsAsync<IEnumerable<Models.Job>>();
                return result;
            }
            else
            {
                throw new Exception($"{resp.ReasonPhrase}");
            }
        }

        public async Task<IEnumerable<Models.JobFile>> GetJobFiles(int id, int type)
        {
            var path = $"{_client.BaseAddress}/api/Job/GetFiles?id={id}&type={type}";
            var resp = await _client.GetAsync(path);
            if (resp.IsSuccessStatusCode)
            {
                var result = await resp.Content.ReadAsAsync<IEnumerable<Models.JobFile>>();
                return result;
            }
            else
            {
                throw new Exception($"{resp.ReasonPhrase}");
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
                    throw new Exception($"{resp.ReasonPhrase}");
                }
            }
        }

        public async Task<Models.JobFile> AddJobFile(int jobId, string filePath, ResultTypeEnum type)
        {
            var path = $"{_client.BaseAddress}/api/Job/AddFile";
            var multiForm = new MultipartFormDataContent();
            multiForm.Add(new StringContent(jobId.ToString()), "JobId");
            multiForm.Add(new StringContent(((int)type).ToString()), "JobFileType");
            
            using (var fs = File.OpenRead(filePath))
            {
                multiForm.Add(new StreamContent(fs), "File", Path.GetFileName(filePath));
                var resp = await _client.PostAsync(path, multiForm);
                if (resp.IsSuccessStatusCode)
                {
                    var result = await resp.Content.ReadAsAsync<Models.JobFile>();
                    return result;
                }
                else
                {
                    throw new Exception($"{resp.ReasonPhrase}");
                }
            }
        }

        public async Task<HttpResponseMessage> DownloadFile(string url, string filePath)
        {            
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            using (var ms = await response.Content.ReadAsStreamAsync())
            {
                using (var fs = File.Create(filePath))
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    ms.CopyTo(fs);
                }
            }

            return response;
        }
    }
}
