using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using StackExchangeService.Interface;
using StackExchangeService.Model;

namespace ApiIsolated
{
    public class QuestionFunction
    {
        private readonly IStackExchangeService _stackExchangeService;
        private readonly ILogger _logger;

        public QuestionFunction(
            IStackExchangeService stackExchangeService,
            ILoggerFactory loggerFactory)
        {
            _stackExchangeService = stackExchangeService;
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("Questions")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var questions = await _stackExchangeService.SearchQuestionsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ToQuestionModel(questions));

            return response;
        }

        private IEnumerable<QuestionModel> ToQuestionModel(IEnumerable<Question> questions)
        {
            return questions.Select(questions =>
            new QuestionModel
            {
                AnswerCount = questions.AnswerCount,
                Id = questions.Id,
                Title = questions.Title
            });
        }
    }
}
