using QuizApi.Domain.Entities;

namespace QuizApi.Data.Repos
{
    public interface ITestVariantRepository
    {
        Task<IEnumerable<TestVariant>> GetAll();
        Task<TestVariant> Get(int id);
        Task<TestVariant> Create(TestVariant testVariant);
        Task<TestVariant> Update(int id, TestVariant testVariant);
        Task<bool> Delete(int id);

    }
}
