using StackExchangeService.Model;

namespace StackExchangeService.Interface
{
    public interface IStackExchangeService
    {
        Task<IEnumerable<Question>> SearchQuestionsAsync(
            int pageSize = 10,
            int answers = 1);

        Task<IEnumerable<Answer>> GetAnswersByQuestionIdAsync(long questionId);
    }
}
