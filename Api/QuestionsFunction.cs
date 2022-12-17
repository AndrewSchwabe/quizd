using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
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

        [Function("GetQuestions")]
        public async Task<HttpResponseData> GetQuestions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "questions")] HttpRequestData req)
        {
            var questions = await _stackExchangeService.SearchQuestionsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ToQuestionModel(questions));

            return response;
        }

        [Function("GetQuestionAnswers")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(
                AuthorizationLevel.Anonymous,
                "get",
                Route = "question/{questionId:long}/answers")] HttpRequestData req,
            long questionId)
        {
            var answers = await _stackExchangeService.GetAnswersByQuestionIdAsync(questionId);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ToAnswerModel(answers));

            return response;
        }

        private IEnumerable<AnswerModel> ToAnswerModel(IEnumerable<Answer> answers)
        {
            return answers.Select(a =>
            new AnswerModel
            {
                Body = a.Body,
                IsAccepted = a.IsAccepted,
            });
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
