using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApi.Domain.Entities
{
    public class Scores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public int TotalScore { get; set; }
        public DateTime SolvedAt { get; set; } = DateTime.Now;

        public string TestVariantId { get; set; }
        public string TestVariantName { get; set;}
    }
}
