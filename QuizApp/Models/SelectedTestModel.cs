using System.Collections.Generic;
using System.Web.Mvc;

namespace QuizApp.Models
{
    public class SelectedTestModel
    {
        public int? TestId { get; set; }
        public string TestTitle { get; set; }
        public string TestDescription { get; set; }
        public int? TestDuration { get; set; }
        public int? QuestionsCount { get; set; }

        public int? CategoryId { get; set; }
        public int? SubcategoryId { get; set; }
        public int? SubSubcategoryId { get; set; }
        public int? QuestionId { get; set; }

        public List<SelectListItem> QuestionsList { get; set; }

        public SelectedTestModel()
        {
            this.QuestionsList = new List<SelectListItem>();            
        }
    }
}