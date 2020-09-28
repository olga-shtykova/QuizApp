using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class User
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }

        // Navigation property
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public User()
        {
            this.Enrollments = new List<Enrollment>();
        }
    }
}