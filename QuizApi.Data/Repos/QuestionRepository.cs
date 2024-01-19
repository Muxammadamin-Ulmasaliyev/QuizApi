using Microsoft.EntityFrameworkCore;
using QuizApi.Domain;
using QuizApi.Domain.Entities;

namespace QuizApi.Data.Repos
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext _appDbContext;
        public QuestionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Question> Create(Question question)
        {
            await _appDbContext.Questions.AddAsync(question);
            await _appDbContext.SaveChangesAsync();
            return question;
        }

        public async Task<bool> Delete(int id)
        {
            var itemToDelete = await _appDbContext.Questions.FindAsync(id);
            if (itemToDelete != null)
            {
                _appDbContext.Questions.Remove(itemToDelete);
                await _appDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Question> Get(int id)
        {
            return await _appDbContext.Questions.FindAsync(id);
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _appDbContext.Questions.ToListAsync();

        }

        public async Task<IEnumerable<Question>> GetQuestionByTestVariantId(int testVariantId)
        {
            var questions = await _appDbContext.Questions.Where(q => q.TestVariantId == testVariantId).ToListAsync();
            return questions;
        }

        public async Task<Question> Update(int id, Question question)
        {
            var updatedQuestion = _appDbContext.Questions.Update(question);
            updatedQuestion.State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return question;
        }
    }
}
