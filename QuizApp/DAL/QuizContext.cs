using QuizApp.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace QuizApp.DAL
{
    public class QuizContext : DbContext
    {
        public QuizContext() : base("DBConnection")
        { 
        
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<SubSubcategory> SubSubcategories { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Test>()
              .HasOptional(s => s.Category)
              .WithMany()
              .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Test>()
              .HasOptional(s => s.Subcategory)
              .WithMany()
              .HasForeignKey(p => p.SubcategoryId);

            modelBuilder.Entity<Test>()
              .HasOptional(s => s.SubSubcategory)
              .WithMany()
              .HasForeignKey(p => p.SubSubcategoryId);

            // configures one-to-many relationship
            modelBuilder.Entity<Choice>()
                .HasRequired(c => c.Question)
                .WithMany(q => q.Choices)
                .HasForeignKey(c => c.QuestionId);
        }
    }
}