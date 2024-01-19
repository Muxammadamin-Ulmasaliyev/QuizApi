using QuizApi.Domain.Entities;

namespace QuizApi.Data.Repos
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAll();
        Task<IEnumerable<Question>> GetQuestionByTestVariantId(int testVariantId);
        Task<Question> Get(int id);
        
        Task<Question> Create(Question question);
        Task<Question> Update(int id, Question question);
        Task<bool> Delete(int id);
    }
}
