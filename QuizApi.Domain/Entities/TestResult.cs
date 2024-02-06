using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizApi.Domain.Entities
{
    public class TestResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        public string Username { get; set; }
        public int TotalScore { get; set; }
        public DateTime SolvedAt { get; set; } = DateTime.Now;

        public int TestVariantId { get; set; }
        public string? TestVariantName { get; set; }
        [JsonIgnore]
        public TestVariant TestVariant { get; set; }
    }
}
