using QuizApi.Data.Repos;
using QuizApi.MapProfiles;
using QuizApi.Models;

namespace QuizApi.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        public QuestionService(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public async Task<QuestionModel> Create(QuestionModel questionModel)
        {
            var question = Mapper.Map(questionModel);
            var createdQuestion = await _questionRepository.Create(question);
            return Mapper.Map(createdQuestion);
        }

        public async Task<bool> Delete(int id)
        {
            return await _questionRepository.Delete(id);
        }

        public async Task<QuestionModel> Get(int id)
        {
            var questionFromDb = await _questionRepository.Get(id); 
            if(questionFromDb != null)
            {
                return Mapper.Map(questionFromDb);
            }
            return null;
        }

        public async Task<IEnumerable<QuestionModel>> GetAll()
        {
            var questionsFromDb = await _questionRepository.GetAll();
            var models = new List<QuestionModel>();
            foreach(var question in questionsFromDb) 
            {
                models.Add(Mapper.Map(question));   
            }
            return models;
        }

        public async Task<IEnumerable<QuestionModel>> GetQuestionByTestVariantId(int testVariantId)
        {
            var questionsFromDb = await _questionRepository.GetQuestionByTestVariantId(testVariantId);
            var models = new List<QuestionModel>();
            foreach (var question in questionsFromDb)
            {
                models.Add(Mapper.Map(question));
            }
            return models;
        }

        public async Task<QuestionModel> Update(int id, QuestionModel questionModel)
        {
            var questionToUpdate = Mapper.Map(questionModel);
            var updatedQuestion = await _questionRepository.Update(id, questionToUpdate);
            return Mapper.Map(updatedQuestion);

        }
    }
}
