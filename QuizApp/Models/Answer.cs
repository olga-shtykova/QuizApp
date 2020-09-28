using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Answer
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int Score { get; set; }

        // Foreign key
        public int? QuestionId { get; set; }

        // Navigation property
        public virtual Question Question { get; set; }
    }
}