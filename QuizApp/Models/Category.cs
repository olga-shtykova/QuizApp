using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Category
    {
        // Primary key
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(65, ErrorMessage = "Максимальная длина - 65 символов.")]
        public string CategoryName { get; set; }

        // Navigation properties
        public virtual ICollection<Subcategory> Subcategories { get; set; }
        public virtual ICollection<Test> Tests { get; set; }

        //public Category()
        //{
        //    this.SubcategoriesList = new List<Subcategory>();
        //    this.TestsList = new List<Test>();
        //}
    }
}