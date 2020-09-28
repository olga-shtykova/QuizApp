using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Имя обязателено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно быть 3 - 50 символов.")]
        [RegularExpression(@"^([A-ZА-Яa-zа-я]+)$", ErrorMessage = "Только прописные и строчные буквы.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилия обязателена")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Фамилия должна быть 3 - 50 символов.")]
        [RegularExpression(@"^([A-ZА-Яa-zа-я]+)$", ErrorMessage = "Только прописные и строчные буквы.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Логин обязателен")]
        [RegularExpression(@"^([a-z0-9]){4,8}", ErrorMessage = "Логин должен быть 4-8 символов a-z и/или 0-9")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Адрес обязателен")]
        [RegularExpression(@"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b", ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }        

        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(18, ErrorMessage = "Пароль должен быть минимум {2} символов длинной.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароль обязателен")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}