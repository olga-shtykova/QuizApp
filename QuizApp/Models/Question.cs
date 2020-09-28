using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models
{
    public class Question
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int Points { get; set; }

        // Foreign key
        [ForeignKey("Test")]
        public int? TestId { get; set; }

        // Navigation properties
        public virtual Test Test { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

        public Question()
        {
            this.Choices = new List<Choice>();
            this.Answers = new List<Answer>();
        }
    }
}