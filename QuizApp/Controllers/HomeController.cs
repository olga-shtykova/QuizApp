using QuizApp.DAL;
using QuizApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace QuizApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly QuizContext _db = new QuizContext();        
        public ActionResult Index()
        {
            return View();
        }
                
        [Authorize]
        public ActionResult QuizPage()
        {           
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult SelectTest()
        {
            TestSelectionModel model = new TestSelectionModel();
            model.CategoriesList = _db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()

            }).ToList();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult SelectTest(int? categoryId, int? subcategoryId, int? subSubcategoryId, int? testId)
        {
            TestSelectionModel model = new TestSelectionModel();

            // Выбор темы  
            model.CategoriesList = _db.Categories.Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()

            }).ToList();                                

            // Выбор подтемы
            if (categoryId.HasValue)
            {
                model.SubcategoriesList = _db.Subcategories
                    .Where(s => s.CategoryId == categoryId.Value)
                    .Select(s => new SelectListItem
                    {
                        Text = s.SubcategoryName,
                        Value = s.Id.ToString()

                    }).ToList();

                // Выбор подподтемы
                if (subcategoryId.HasValue)
                {                  
                    model.SubSubcategoriesList = _db.SubSubcategories
                    .Where(s => s.SubcategoryId == subcategoryId.Value)
                    .Select(s => new SelectListItem
                    {
                        Text = s.SubSubcategoryName,
                        Value = s.Id.ToString()

                    }).ToList();
                }

                // Выбор теста
                if (subSubcategoryId.HasValue)
                {
                    model.TestsList = _db.Tests
                   .Where(s => s.SubSubcategoryId == subSubcategoryId.Value)
                   .Select(s => new SelectListItem
                   {
                       Text = s.TestTitle,
                       Value = s.Id.ToString()

                   }).ToList();                    
                }
            }
            if (testId.HasValue)
            {
                var testSelected = _db.Tests.Where(t => t.Id == testId.Value)
                   .Select(t => new TestSelectionModel
                   {
                       TestId = t.Id,
                       TestTitle = t.TestTitle,
                       TestDescription = t.Description,
                       QuestionsCount = t.Questions.Where(q => q.TestId == model.TestId).Count(),
                       TestDuration = t.Duration
                   }).FirstOrDefault();                   

                if (testSelected != null)
                {
                    Session["SelectedQuiz"] = testSelected;
                    
                    return RedirectToAction("TestInstruction", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult TestInstruction()
        {
            TestSelectionModel model = Session["SelectedQuiz"] as TestSelectionModel;

            if (ModelState.IsValid)
            {
                var testSelected = _db.Tests.Where(t => t.Id == model.TestId).FirstOrDefault();
                if (testSelected != null)
                {
                    ViewBag.TestTitle = testSelected.TestTitle;
                    ViewBag.TestDescription = testSelected.Description;
                    ViewBag.QuestionCount = _db.Questions.Where(q => q.TestId == model.TestId).Count();
                    ViewBag.TestDuration = testSelected.Duration;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult TestInstruction(TestSelectionModel model)
        {
            if (ModelState.IsValid)
            {
                var testSelected = _db.Tests.Where(t => t.Id == model.TestId).FirstOrDefault();
                if (testSelected != null)
                {
                    ViewBag.TestTitle = testSelected.TestTitle;
                    ViewBag.TestDescription = testSelected.Description;
                    ViewBag.QuestionCount = _db.Questions.Where(q => q.TestId == model.TestId).Count();
                    ViewBag.TestDuration = testSelected.Duration;
                }
            }
            return View(); 
        }

        [HttpPost]
        public ActionResult Enrollment(TestSelectionModel model)
        {

            return RedirectToAction("TestPage", new { @Token = Session["Token"] });
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
    
}