using QuizApi.Domain.Entities;
using QuizApi.Models;

namespace QuizApi.Services
{
    public interface ITestVariantService
    {
        Task<IEnumerable<TestVariantModel>> GetAll();
        Task<TestVariantModel> Get(int id);
        Task<TestVariantModel> Create(TestVariantModel testVariantModel);
        Task<TestVariantModel> Update(int id, TestVariantModel testVariantModel);
        Task<bool> Delete(int id);
    }
}