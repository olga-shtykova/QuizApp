using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace QuizApp.Models
{
    public class EnrollmentModel
    {
        public DateTime EnrollmentDate { get; set; }
        public Guid Token { get; set; }
        public DateTime TokenExpirationTime { get; set; }

        // Foreign key
        public int? UserId { get; set; }
        public string UserLogin { get; set; }
        public int? TestId { get; set; }

        public List<SelectListItem> TestsList { get; set; }

        public EnrollmentModel()
        {
            this.TestsList = new List<SelectListItem>();
        }
    }
}