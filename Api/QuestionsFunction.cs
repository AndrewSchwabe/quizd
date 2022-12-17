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

        public QuestionFunction(
            IStackExchangeService stackExchangeService)
        {
            _stackExchangeService = stackExchangeService;
        }

        [Function("GetQuestions")]
        public async Task<HttpResponseData> GetQuestions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "question")] HttpRequestData req)
        {
            var questions = await _stackExchangeService.SearchQuestionsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ToQuestionModel(questions));

            return response;
        }

        [Function("GetQuestionById")]
        public async Task<HttpResponseData> GetQuestionById(
            [HttpTrigger(
                AuthorizationLevel.Anonymous, 
                "get", 
                Route = "question/{questionId:long}")] HttpRequestData req,
            long questionId)
        {
            var question = await _stackExchangeService.GetQuestionByIdAsync(questionId);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(ToQuestionModel(question));

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
            return questions.Select(ToQuestionModel);
        }

        private QuestionModel ToQuestionModel(Question question)
        {
            return new QuestionModel
            {
                AnswerCount = question.AnswerCount,
                Id = question.Id,
                Title = question.Title
            };
        }
    }
}
