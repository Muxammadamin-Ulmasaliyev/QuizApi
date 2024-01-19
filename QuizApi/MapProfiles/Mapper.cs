using QuizApi.Domain.Entities;
using QuizApi.Models;

namespace QuizApi.MapProfiles
{
    public static class Mapper
    {
        public static UserProfileModel Map(AppUser userFromDb)
        {
            return new UserProfileModel()
            {
                Id = userFromDb.Id,
                Username = userFromDb.UserName,
                Email = userFromDb.Email,
                PhoneNumber = userFromDb.PhoneNumber,
                FullName = userFromDb.FullName,
            };
        }

        public static TestVariant Map(TestVariantModel model)
        {
            return new TestVariant()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }
        public static TestVariantModel Map(TestVariant testVariant)
        {
            return new TestVariantModel()
            {
                Id = testVariant.Id,
                Name = testVariant.Name,
                Description = testVariant.Description,
                Questions = testVariant.Questions

            };
        }

        public static Question Map(QuestionModel model)
        {
            return new Question()
            {
                Id = model.Id,
                Text = model.Text,
                A = model.A,
                B = model.B,
                C = model.C,
                D = model.D,
                CorrectAnswer = model.CorrectAnswer,
                TestVariantId = model.TestVariantId
            };
        }
        public static QuestionModel Map(Question question)
        {
            return new QuestionModel()
            {
                Id = question.Id,
                Text = question.Text,
                A = question.A,
                B = question.B,
                C = question.C,
                D = question.D,
                CorrectAnswer = question.CorrectAnswer,
                TestVariantId = question.TestVariantId,
                TestVariant = question.TestVariant
            };
        }
    }
}
