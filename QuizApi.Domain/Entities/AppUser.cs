using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
namespace QuizApi.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        [JsonIgnore]
        public List<TestResult> TestResults { get; set; }

    }
}
