using QuizApp.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace QuizApp.DAL
{
    public class QuizInitializer : DropCreateDatabaseAlways<QuizContext>
    {
        protected override void Seed(QuizContext db)
        {
            var users = new List<User>
            {
                new User
                { 
                    FirstName = "Иван", 
                    LastName = "Шишкин", 
                    Login = "Ivan", 
                    Password = "12345", 
                    Email = "ivan@mail.ru" 
                }
            };
            users.ForEach(s => db.Users.Add(s));
            db.SaveChanges();

            var categories = new List<Category>
            {
                new Category {CategoryName = "Тесты по наукам"},
                new Category {CategoryName = "Тесты на эрудицию и знания"}
            };
            categories.ForEach(s => db.Categories.Add(s));
            db.SaveChanges();

            var subcategories = new List<Subcategory>
            {
                new Subcategory {SubcategoryName = "Тесты по психологии", CategoryId = 1},
                new Subcategory {SubcategoryName = "Тесты о животных", CategoryId = 2}
            };
            subcategories.ForEach(s => db.Subcategories.Add(s));
            db.SaveChanges();

            var subSubcategories = new List<SubSubcategory>
            {
                new SubSubcategory {SubSubcategoryName = "IQ тесты", SubcategoryId = 1},
                new SubSubcategory {SubSubcategoryName = "Тесты о кошках", SubcategoryId = 2}
            };
            subSubcategories.ForEach(s => db.SubSubcategories.Add(s));
            db.SaveChanges();

            var tests = new List<Test>
            {
                new Test
                {
                    TestTitle= "Психологический IQ",
                    Description = "Тест проверяет базовые знания по психологии.",
                    Duration = 10,
                    CategoryId = 1,
                    SubcategoryId = 1,
                    SubSubcategoryId = 1
                },
                new Test
                {
                    TestTitle = "Вы хорошо понимаете кошачий язык",
                    Description = "Все владельцы кошек думают, что неплохо разбираются в поведении кошек. Так ли это на самом деле?",
                    Duration = 5,
                    CategoryId = 2,
                    SubcategoryId = 2,
                    SubSubcategoryId = 2
                }
            };
            tests.ForEach(s => db.Tests.Add(s));
            db.SaveChanges();

            var questions = new List<Question>
            {
                new Question {QuestionText = "Шизофрения это -", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Основы физической привлекательности для потенциальных романтических партнеров?", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Детектор лжи...", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Какое утверждение верно в отношении человеческого мозга?", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Как ведуд себя люди под гипнозом?", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Лучший способ справиться с гневом и агрессией - это спорт и физические нагрузки", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Счастливый работник - продуктивный работник", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Красивые люди кажутся другим более умными и успешными, чем некрасивые", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Как называется феномен, когда заложник начинает сопереживать своему похитителю?", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "В темной комнате точка света будет казаться движущейся. Эта иллюзия называется...", QuestionType = "Radio", Points = 10, TestId = 1},
                new Question {QuestionText = "Хвост кошки стоит «трубой», а его кончик выпрямлен…", QuestionType = "Checkbox", Points = 20, TestId = 2},
                new Question {QuestionText = "Кошка бьет хвостом об пол", QuestionType = "Checkbox", Points = 20, TestId = 2},
                new Question {QuestionText = "Что вы слышите в кошачьем шипении?", QuestionType = "Checkbox", Points = 20, TestId = 2},
                new Question {QuestionText = "В какой позе питомец расслаблен?", QuestionType = "Checkbox", Points = 20, TestId = 2},
                new Question {QuestionText = "Кошка обычно прижимает уши к голове, когда…", QuestionType = "Checkbox", Points = 20, TestId = 2},
            };
            questions.ForEach(s => db.Questions.Add(s));
            db.SaveChanges();
            var choices = new List<Choice>
            {
                new Choice {ChoiceText = "Связано с резкими перепадами настроения и насилием", Points = 0, QuestionId = 1},
                new Choice {ChoiceText = "Когда слышишь голоса", Points = 10, QuestionId = 1},
                new Choice {ChoiceText = "Раздвоение личности", Points = 0, QuestionId = 1},
                new Choice {ChoiceText = "Пробой между мыслями, эмоциями и поведением", Points = 0, QuestionId = 1},
                new Choice {ChoiceText = "Ключ к успеху - общие ценности и убеждения", Points = 10, QuestionId = 2},
                new Choice {ChoiceText = "Ее сильные стороны должны компенсировать мои слабости", Points = 0, QuestionId = 2},
                new Choice {ChoiceText = "Химия тела - это наше все", Points = 0, QuestionId = 2},
                new Choice {ChoiceText = "Противоположности притягиваются", Points = 0, QuestionId = 2},
                new Choice {ChoiceText = "Часто ошибается в отношении невиновных людей", Points = 0, QuestionId = 3},
                new Choice {ChoiceText = "Точен на 90%", Points = 0, QuestionId = 3},
                new Choice {ChoiceText = "Шансы на правду с ним - 50 на 50", Points = 10, QuestionId = 3},
                new Choice {ChoiceText = "Может изучать только психопатов", Points = 0, QuestionId = 3},
                new Choice {ChoiceText = "Все наше прошлое хранится в нашей памяти, но мы не всегда можем это отыскать", Points = 0, QuestionId = 4},
                new Choice {ChoiceText = "Мы используем только 10% возможностей своего мозга", Points = 10, QuestionId = 4},
                new Choice {ChoiceText = "Мы очень мало знаем о том, как работает наш мозг", Points = 0, QuestionId = 4},
                new Choice {ChoiceText = "10% - это чушь. Мы используем все отделы своего мозга", Points = 0, QuestionId = 4},
                new Choice {ChoiceText = "Они не понимают, что происходит вокруг", Points = 0, QuestionId = 5},
                new Choice {ChoiceText = "Примерно как пьяные", Points = 0, QuestionId = 5},
                new Choice {ChoiceText = "Как во сне", Points = 10, QuestionId = 5},
                new Choice {ChoiceText = "Они будто проснулись по звонку будильника", Points = 0, QuestionId = 5},
                new Choice {ChoiceText = "Правда", Points = 10, QuestionId = 6},
                new Choice {ChoiceText = "Ложь", Points = 0, QuestionId = 6},
                new Choice {ChoiceText = "Ложь", Points = 0, QuestionId = 7},
                new Choice {ChoiceText = "Правда", Points = 10, QuestionId = 7},
                new Choice {ChoiceText = "Нет", Points = 0, QuestionId = 8},
                new Choice {ChoiceText = "Без понятия", Points = 0, QuestionId = 8},
                new Choice {ChoiceText = "Да", Points = 10, QuestionId = 8},
                new Choice {ChoiceText = "Ничего из перечисленного", Points = 0, QuestionId = 9},
                new Choice {ChoiceText = "Стокгольмский синдром", Points = 10, QuestionId = 9},
                new Choice {ChoiceText = "Синдром Осло", Points = 0, QuestionId = 9},
                new Choice {ChoiceText = "Копенгагенский синдром", Points = 0, QuestionId = 9},
                new Choice {ChoiceText = "Теория оптического движения", Points = 10, QuestionId = 10},
                new Choice {ChoiceText = "Иллюзия Позно", Points = 0, QuestionId = 10},
                new Choice {ChoiceText = "Автокинетическая иллюзия", Points = 0, QuestionId = 10},
                new Choice {ChoiceText = "Рефлекс Пуркинье", Points = 0, QuestionId = 10},
                new Choice {ChoiceText = "Не в самом лучшем настроении", Points = 0, QuestionId = 11},
                new Choice {ChoiceText = "Рада меня видеть", Points = 10, QuestionId = 11},
                new Choice {ChoiceText = "У кошки хорошее настроение", Points = 10, QuestionId = 11},
                new Choice {ChoiceText = "Она просто в бешенстве", Points = 10, QuestionId = 12},
                new Choice {ChoiceText = "Отстань от меня", Points = 10, QuestionId = 12},
                new Choice {ChoiceText = "Она охотится", Points = 0, QuestionId = 12},
                new Choice {ChoiceText = "«Кругом враги, их надо напугать»", Points = 0, QuestionId = 13},
                new Choice {ChoiceText = "«Помогите, мне больно»", Points = 10, QuestionId = 13},
                new Choice {ChoiceText = "«Мне страшно»", Points = 10, QuestionId = 13},
                new Choice {ChoiceText = "На животе, поджав передние лапки под себя", Points = 0, QuestionId = 14},
                new Choice {ChoiceText = "На боку, вытянув лапы", Points = 10, QuestionId = 14},
                new Choice {ChoiceText = "Свернувшись клубком", Points = 10, QuestionId = 14},
                new Choice {ChoiceText = "Мерзнет", Points = 0, QuestionId = 15},
                new Choice {ChoiceText = "Ей страшно, но она готова защищаться", Points = 10, QuestionId = 15},
                new Choice {ChoiceText = "Она хочет, чтобы вы ее отпустили из рук", Points = 10, QuestionId = 15},
            };
            choices.ForEach(s => db.Choices.Add(s));
            db.SaveChanges();
        }
    }
}