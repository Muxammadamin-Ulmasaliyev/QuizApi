using Microsoft.EntityFrameworkCore;
using QuizApi.Domain;
using QuizApi.Domain.Entities;

namespace QuizApi.Data.Repos
{
    public class TestVariantRepository : ITestVariantRepository
    {
        private readonly AppDbContext _appDbContext;
        public TestVariantRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TestVariant> Create(TestVariant testVariant)
        {
            await _appDbContext.TestVariants.AddAsync(testVariant);
            await _appDbContext.SaveChangesAsync();
            return testVariant;
        }

        public async Task<bool> Delete(int id)
        {
            var itemToDelete = await _appDbContext.TestVariants.FindAsync(id);
            if (itemToDelete != null)
            {
                _appDbContext.TestVariants.Remove(itemToDelete);
                await _appDbContext.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<IEnumerable<TestVariant>> GetAll()
        {
            return await _appDbContext.TestVariants.ToListAsync();
        }

        public async Task<TestVariant> Get(int id)
        {
            var testVariant = await _appDbContext.TestVariants.Include(tv => tv.Questions).FirstOrDefaultAsync(tv => tv.Id == id);
            return testVariant;
            // var tvs =  await _appDbContext.TestVariants.Include(tv => tv.Questions).ToListAsync();
            //return tvs.Where(t =>t.Id == id).FirstOrDefault();
        }

        public async Task<TestVariant> Update(int id, TestVariant testVariant)
        {
            var updatedTestVariant = _appDbContext.TestVariants.Update(testVariant);
            updatedTestVariant.State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return testVariant;
        }
    }
}
