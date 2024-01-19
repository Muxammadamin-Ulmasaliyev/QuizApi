using Microsoft.EntityFrameworkCore;
using QuizApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace QuizApi.Domain
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }


        public DbSet<TestVariant> TestVariants { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Scores> Scores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestVariant>()
                         .Property(e => e.Id)
                         .ValueGeneratedOnAdd();

            modelBuilder.Entity<Question>()
                        .HasOne(q => q.TestVariant)
                        .WithMany(tv => tv.Questions)
                        .HasForeignKey(q => q.TestVariantId);

            base.OnModelCreating(modelBuilder);

        }
    }
}
