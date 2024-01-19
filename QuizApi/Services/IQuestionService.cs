using QuizApi.Domain.Entities;
using QuizApi.Models;

namespace QuizApi.Services
{
    public interface IQuestionService
    {
        Task<IEnumerable<QuestionModel>> GetAll();
        Task<IEnumerable<QuestionModel>> GetQuestionByTestVariantId(int testVariantId);

        Task<QuestionModel> Get(int id);
        Task<QuestionModel> Create(QuestionModel questionModel);
        Task<QuestionModel> Update(int id, QuestionModel questionModel);
        Task<bool> Delete(int id);
    }
}
