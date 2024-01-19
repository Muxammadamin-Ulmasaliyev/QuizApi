using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizApi.Domain.Entities
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }

        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string CorrectAnswer { get; set; } // A,B,C,D
        public int TestVariantId { get; set; }
        [JsonIgnore]
        public TestVariant TestVariant { get; set; }

    }
}
