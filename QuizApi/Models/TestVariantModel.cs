using QuizApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class TestVariantModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int NumberOfQuestions { get; set; }

        public List<Question>? Questions { get; set; }
    }
}
