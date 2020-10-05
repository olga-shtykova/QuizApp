using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class UserAnswer
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }

        // Foreign key
        public int? EnrollmentId { get; set; }
        public int? QuestionId { get; set; }
        public int? ChoiceId { get; set; }

        // Navigation property
        public virtual Enrollment Enrollment { get; set; }
        public virtual Question Question { get; set; }
        public virtual Choice Choice { get; set; }
    }
}