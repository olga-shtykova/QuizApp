using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Choice
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public string ChoiceText { get; set; }
        public int Points { get; set; }

        // Foreign key
        public int QuestionId { get; set; } 

        // Navigation property
        public virtual Question Question { get; set; }
    }
}