using StackExchangeService.Model;

namespace StackExchangeService.Interface
{
    public interface IStackExchangeService
    {
        Task<IEnumerable<Question>> SearchQuestionsAsync(
            int pageSize = 10,
            int answers = 2);

        Task<Question> GetQuestionByIdAsync(long questionId);

        Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(long questionId);
    }
}
