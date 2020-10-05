using QuizApp.DAL;
using QuizApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace QuizApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly QuizContext _db = new QuizContext();

        [HttpGet]
        public ActionResult Register()
        {
            // при попытке регистрации авторизованного пользователя
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // ищем пользователя с заданным логином
                var userConnected = _db.Users
                    .FirstOrDefault(u => u.Login == model.Login);

                // если логин свободен - создаем пользователя
                if (userConnected == null)
                {
                    // сохраняем пользователя в базе данных
                    _db.Users.Add(new User 
                    { 
                        FirstName = model.FirstName, 
                        LastName = model.LastName, 
                        Login = model.Login, 
                        Password = model.Password, 
                        Email = model.Email 
                    });
                    _db.SaveChanges();

                    // проверяем, что пользователь создан
                    userConnected = _db.Users
                        .FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

                    // если пользователь создан - перенаправляем его на ListChats
                    if (userConnected != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);
                        return RedirectToAction("QuizPage", "Home");
                    }
                }

                ModelState.AddModelError("", "Логин занят!");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            // при попытке повторного входа авторизованного пользователя
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                // ищем пользователя с заданным логином и паролем
                var userConnected = _db.Users
                    .FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);

                // если он есть - перенаправляем его на ListChats
                if (userConnected != null)
                {
                    // аутентификация пользователя
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("QuizPage", "Home");
                }

                ModelState.AddModelError("", "Неправильный логин или пароль!");
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}