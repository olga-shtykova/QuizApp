using System.Collections.Generic;
using System.Web.Mvc;

namespace QuizApp.Models
{
    public class TestSelectionModel
    {
        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? SubSubcategoryId { get; set; }
        public int? TestId { get; set; }

        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string SubSubcategoryName { get; set; }
        public string TestTitle { get; set; }
        public string TestDescription { get; set; }
        public int? TestDuration { get; set; }
        public int? QuestionsCount { get; set; }

        public List<SelectListItem> CategoriesList { get; set; }
        public List<SelectListItem> SubcategoriesList { get; set; }
        public List<SelectListItem> SubSubcategoriesList { get; set; }
        public List<SelectListItem> TestsList { get; set; }        

        public TestSelectionModel()
        {
            this.CategoriesList = new List<SelectListItem>();
            this.SubcategoriesList = new List<SelectListItem>();
            this.SubSubcategoriesList = new List<SelectListItem>();
            this.TestsList = new List<SelectListItem>();
        }
    }
}
