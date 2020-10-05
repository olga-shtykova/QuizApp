namespace QuizApp.Models
{
    public class ChoiceModel
    {
        public int? ChoiceId { get; set; }
        public string ChoiceText { get; set; }
        public string Answer { get; set; }
        public bool IsSelected { get; set; }
    }
}