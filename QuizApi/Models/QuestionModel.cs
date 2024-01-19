using QuizApi.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string A { get; set; }
        [Required]
        public string B { get; set; }
        [Required]
        public string C { get; set; }
        [Required]
        public string D { get; set; }
        [Required]
        public string CorrectAnswer { get; set; } // A,B,C,D
        [Required]
        public int TestVariantId { get; set; }
        public TestVariant? TestVariant { get; set; }
    }
}
