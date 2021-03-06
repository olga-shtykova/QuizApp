﻿using System;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Enrollment
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Guid Token { get; set; }
        public DateTime TokenExpirationTime { get; set; }

        // Foreign key
        public int? UserId { get; set; }
        public int? TestId { get; set; }

        // Navigation property
        public virtual User User { get; set; }        
        public virtual Test Test { get; set; }
    }
}