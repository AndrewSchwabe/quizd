using StackExchangeService.Interface;
using StackExchangeService.Model;
using System.Diagnostics;
using System.IO.Compression;
using System.Text.Json;

namespace StackExchangeService
{
    public class StackExchangeService : IStackExchangeService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StackExchangeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<Question>> SearchQuestionsAsync(int pageSize = 10, int minAnswerCount = 1)
        {
            var client = _httpClientFactory.CreateClient("stackexchange");

            var response = await client.GetAsync($"/2.3/search/advanced?pagesize={pageSize}&order=desc&sort=activity&accepted=true&answers={minAnswerCount}&site=stackoverflow");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await GetStringResponseContent(response);

                return JsonSerializer.Deserialize<Response<Question>>(responseString)?.Items ?? new List<Question>();
            }

            return new List<Question>();
        }

        public async Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(long questionId)
        {
            var client = _httpClientFactory.CreateClient("stackexchange");

            var response = await client.GetAsync($"/2.3/questions/{questionId}/answers?order=desc&sort=activity&site=stackoverflow&filter=!nOedRLqQ19");
            if (response.IsSuccessStatusCode)
            {
                string responseString = await GetStringResponseContent(response);

                return JsonSerializer.Deserialize<Response<Answer>>(responseString)?.Items ?? new List<Answer>();
            }

            return new List<Answer>();
        }

        private async Task<string> GetStringResponseContent(HttpResponseMessage response)
        {
            using (var responseStream = response.Content.ReadAsStream())
            using (var gzipStream = new GZipStream(responseStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(gzipStream))
            {
                return await streamReader.ReadToEndAsync();
            }
        }


    }
}
