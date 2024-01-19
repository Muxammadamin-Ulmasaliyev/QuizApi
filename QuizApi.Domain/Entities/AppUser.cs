using Microsoft.AspNetCore.Identity;
namespace QuizApi.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

    }
}
