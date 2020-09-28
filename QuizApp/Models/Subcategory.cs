using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Subcategory
    {      
        // Primary key
        [Key]
        public int Id { get; set; }

        [StringLength(65, ErrorMessage = "Максимальная длина - 65 символов.")]
        public string SubcategoryName { get; set; }

        // Foreign key
        public int? CategoryId { get; set; }

        // Navigation properties
        public virtual Category Category { get; set; }
        public virtual ICollection<SubSubcategory> SubSubcategories { get; set; }
        public virtual ICollection<Test> Tests { get; set; }

        //public Subcategory()
        //{
        //    this.SubSubcategorieList = new List<SubSubcategory>();
        //    this.TestsList = new List<Test>();
        //}
    }
}