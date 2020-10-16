using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Test
    {
        // Primary key
        [Key]
        public int Id { get; set; }
        public string TestTitle { get; set; }

        [StringLength(100, ErrorMessage = "Максимальная длина - 100 символов.")]
        public string Description { get; set; }
        //public int Duration { get; set; }
        public int Duration { get; set; }

        // Foreign keys        
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? SubSubcategoryId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }       
        public virtual Subcategory Subcategory { get; set; }   
        public virtual SubSubcategory SubSubcategory { get; set; }
        public virtual ICollection<Question> Questions { get; set; }

        public Test()
        {
            this.Questions = new List<Question>();
        }
    }
}