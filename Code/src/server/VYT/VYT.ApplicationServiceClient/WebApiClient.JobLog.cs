using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VYT.ApplicationServiceClient
{
    public partial class WebApiClient
    {        
        public async Task<IEnumerable<Models.JobLog>> GetJobLogPage(int pageIndex, int pageSize)
        {
            var path = $"{_client.BaseAddress}/api/JobLog?pageIndex={pageIndex}&pageSize={pageSize}";
            var response = await _client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<IEnumerable<Models.JobLog>>();
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> GetTotalJobLogs()
        {
            var path = $"{_client.BaseAddress}/api/JobLog/Total";
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

        public async Task<Models.JobLog> AddJobLog(Models.JobLog jobLog)
        {
            var path = $"{_client.BaseAddress}/api/JobLog";
            var response = await _client.PostAsJsonAsync(path, jobLog);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<Models.JobLog>();
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
