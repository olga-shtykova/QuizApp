using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class SubSubcategory
    {
        // Primary key
        [Key]
        public int Id { get; set; }

        [StringLength(65, ErrorMessage = "Максимальная длина - 65 символов.")]
        public string SubSubcategoryName { get; set; }

        // Foreign key
        public int? SubcategoryId { get; set; }

        // Navigation properties
        public virtual Subcategory Subcategory { get; set; }
        public virtual ICollection<Test> Tests { get; set; }

        //public SubSubcategory()
        //{
        //    this.TestsList = new List<Test>();
        //}
    }
}