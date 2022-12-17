using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BlazorApp.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ApiIsolated
{
    public class QuestionFunction
    {
        private readonly ILogger _logger;

        public QuestionFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpTrigger>();
        }

        [Function("Questions")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
        {
            var result = new List<Question>
            {
                new Question
                {
                Id = 0,
                Title = $"Title-red{Guid.NewGuid()}",
                AnswerCount = 4
                }
            };

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(result);

            return response;
        }
    }
}
