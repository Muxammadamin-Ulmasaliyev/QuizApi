using QuizApi.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizApi.Models
{
    public class TestResultModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string? Username { get; set; }
        [Required]
        public int TotalScore { get; set; }
        public DateTime SolvedAt { get; set; } 
        [Required]
        public int TestVariantId { get; set; }
        public string? TestVariantName { get; set; }
    }
}
