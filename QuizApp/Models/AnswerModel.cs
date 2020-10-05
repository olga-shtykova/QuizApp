using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace QuizApp.Models
{
    public class AnswerModel
    {
        public int? TestId { get; set; }
        public int? QuestionId { get; set; }       
        public Guid Token { get; set; }
        public string Direction { get; set; }
        public string Answer { get; set; }
        public List<ChoiceModel> UserChoices { get; set; }

        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int? QPoints { get; set; }
        public int TotalQuestionsCount { get; set; }
        public string TestTitle { get; set; }        
    }
}