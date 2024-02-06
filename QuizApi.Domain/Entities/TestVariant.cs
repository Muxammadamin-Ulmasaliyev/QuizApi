using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizApi.Domain.Entities
{
    public class TestVariant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public List<Question>? Questions { get; set; }
        [JsonIgnore]
        public List<TestResult> TestResults { get; set; }
    }
}
