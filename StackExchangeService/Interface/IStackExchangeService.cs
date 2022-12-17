using StackExchangeService.Model;

namespace StackExchangeService.Interface
{
    public interface IStackExchangeService
    {
        Task<IEnumerable<Question>> SearchQuestionsAsync(
            int pageSize = 10,
            int answers = 1);
    }
}
