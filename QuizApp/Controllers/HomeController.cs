using QuizApp.DAL;
using QuizApp.Models;
using System;
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
                    // Записываем в состояние сеанса информацию выбранном тесте
                    Session["SelectedTest"] = testSelected;

                    return RedirectToAction("TestInstruction", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult TestInstruction()
        {
            TestSelectionModel model = Session["SelectedTest"] as TestSelectionModel;

            if (ModelState.IsValid)
            {
                var testSelected = _db.Tests.FirstOrDefault(t => t.Id == model.TestId);
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
        [Authorize]
        public ActionResult TestInstruction(TestSelectionModel model)
        {
            if (ModelState.IsValid)
            {
                var testSelected = _db.Tests.FirstOrDefault(t => t.Id == model.TestId);
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
        [Authorize]
        public ActionResult Enrollment(TestSelectionModel model)
        {
            if (ModelState.IsValid)
            {
                var testSelected = _db.Tests.FirstOrDefault(t => t.Id == model.TestId);

                // Получаем логин пользователя
                var userLogin = User.Identity.Name;

                // ищем Id пользователя
                var userConnected = _db.Users
                    .FirstOrDefault(u => u.Login == userLogin);

                if (userConnected == null)
                {
                    return HttpNotFound();
                }

                Enrollment enrollment = _db.Enrollments
                .FirstOrDefault(e => e.UserId == userConnected.Id && e.TestId == model.TestId
                && e.TokenExpirationTime > DateTime.UtcNow);

                if (enrollment != null)
                {
                    this.Session["Token"] = enrollment.Token;
                    this.Session["TokenExpiration"] = enrollment.TokenExpirationTime;
                }

                if (testSelected != null)
                {
                    Enrollment newEnrollment = new Enrollment()
                    {
                        UserId = userConnected.Id,
                        EnrollmentDate = DateTime.UtcNow,
                        TestId = model.TestId,
                        Token = Guid.NewGuid(),
                        TokenExpirationTime = DateTime.UtcNow.AddMinutes(testSelected.Duration)
                    };
                    userConnected.Enrollments.Add(newEnrollment);
                    _db.Enrollments.Add(newEnrollment);
                    _db.SaveChanges();

                    this.Session["Token"] = newEnrollment.Token;
                    this.Session["TokenExpiration"] = newEnrollment.TokenExpirationTime;
                }
                return RedirectToAction("TestPage", new { Token = Session["Token"] });
            }
            return View();             
        }

        [HttpGet]
        [Authorize]
        public ActionResult TestPage(Guid token, int? qnum)
        {
            if (token == null)
            {
                return RedirectToAction("SelectTest");
            }

            //Проверяем, что пользователь зарегистрирован пройти тест
            var enrollment = _db.Enrollments.FirstOrDefault(e => e.Token.Equals(token));

            if (enrollment == null)
            {
                ModelState.AddModelError("", "Неверный токен!");
                return RedirectToAction("SelectTest");
            }
            if (enrollment.TokenExpirationTime < DateTime.UtcNow)
            {
                ModelState.AddModelError("", $"Время для прохождения теста истекло: {enrollment.TokenExpirationTime}");
                return RedirectToAction("SelectTest");
            }

            if (qnum.GetValueOrDefault() < 1)
                qnum = 1;

            var testQuestionId = _db.Questions
               .Where(q => q.TestId == enrollment.TestId && q.QuestionNumber == qnum)
               .Select(q => q.Id).FirstOrDefault();

            if (testQuestionId > 0)
            {
                var model = _db.Questions.Where(q => q.Id == testQuestionId)
                    .Select(q => new AnswerModel
                    {
                        QuestionType = q.QuestionType,
                        QuestionNumber = q.QuestionNumber,
                        QuestionText = q.QuestionText,
                        QPoints = q.Points,
                        TestId = q.TestId,
                        TestTitle = q.Test.TestTitle,
                        UserChoices = q.Choices.Select(c => new ChoiceModel
                        {
                            ChoiceId = c.Id,
                            ChoiceText = c.ChoiceText
                        }).ToList()
                    }).FirstOrDefault();    

                model.TotalQuestionsCount = _db.Questions
                    .Where(q => q.TestId == enrollment.TestId).Count();

                ViewBag.TimeExpire = enrollment.TokenExpirationTime;          

                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult TestPage(AnswerModel model)
        {
            if (ModelState.IsValid)
            {
                var enrollment = _db.Enrollments.Where(x => x.Token.Equals(model.Token)).FirstOrDefault();

                var testQuestionInfo = _db.Questions.Where(q => q.TestId == enrollment.TestId && q.QuestionNumber == model.QuestionId)
                    .Select(q => new
                    {
                        q.Id,
                        q.QuestionType,
                        Point = q.Points
                    }).FirstOrDefault();         
            }
            // Показать следующий или предыдущий вопрос

            var nextQuestionNumber = 1;

            if (model.Direction.Equals("next", StringComparison.CurrentCultureIgnoreCase))
            {
                nextQuestionNumber = _db.Questions.Where(q => q.TestId == model.TestId
                && q.QuestionNumber > model.QuestionId)
                .OrderBy(x => x.QuestionNumber).Take(1).Select(x => x.QuestionNumber).FirstOrDefault();
            }
            else
            {
                nextQuestionNumber = _db.Questions.Where(x => x.TestId == model.TestId
                && x.QuestionNumber < model.QuestionId)
                .OrderByDescending(x => x.QuestionNumber).Take(1).Select(x => x.QuestionNumber).FirstOrDefault();
            }

            if (nextQuestionNumber < 1)
            {
                // После последнего вопроса перенаправляем на результат теста
                return RedirectToAction("TestResult", new { @token = Session["Token"] });
            }
            else
            {
                return RedirectToAction("TestPage", new
                {
                    @token = Session["Token"],
                    @qnum = nextQuestionNumber
                });
            }
        }

        [Authorize]
        public ActionResult TestResult()
        {

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }

}