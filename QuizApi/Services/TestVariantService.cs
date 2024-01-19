using QuizApi.Data.Repos;
using QuizApi.MapProfiles;
using QuizApi.Models;

namespace QuizApi.Services
{
    public class TestVariantService : ITestVariantService
    {
        private readonly ITestVariantRepository _testVariantRepository;
        public TestVariantService(ITestVariantRepository testVariantRepository)
        {
            _testVariantRepository = testVariantRepository;
        }
        public async Task<TestVariantModel> Create(TestVariantModel testVariantModel)
        {
            var testVariant = Mapper.Map(testVariantModel);
            var createdTestVariant = await _testVariantRepository.Create(testVariant);
            return Mapper.Map(createdTestVariant);

        }

        public async Task<bool> Delete(int id)
        {
            return await _testVariantRepository.Delete(id);
        }

        public async Task<TestVariantModel> Get(int id)
        {
            var testVariantFromDb = await _testVariantRepository.Get(id);
            if(testVariantFromDb != null)
            {
                return Mapper.Map(testVariantFromDb);
            }
            return null;
        }

        public async Task<IEnumerable<TestVariantModel>> GetAll()
        {

            var testVariantsFromDb = await _testVariantRepository.GetAll();
            var models = new List<TestVariantModel>();
            foreach (var testVariant in testVariantsFromDb)
            {
                models.Add(Mapper.Map(testVariant));
            }
            return models;

        }

        public async Task<TestVariantModel> Update(int id, TestVariantModel testVariantModel)
        {
            var testVariantToUpdate = Mapper.Map(testVariantModel);
            var updatedTestVariant = await _testVariantRepository.Update(id, testVariantToUpdate);
            return Mapper.Map(updatedTestVariant);  
        }
    }
}
