namespace QuizAppRazor.Models.ViewModels
{
    public class QuestionInputModel
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public List<TextInputModel> Answers { get; set; } = new List<TextInputModel>();
    }
}
