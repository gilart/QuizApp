namespace QuizAppRazor.Models.ViewModels
{
    public class QuizCreateViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<QuestionInputModel> Questions { get; set; } = new List<QuestionInputModel>();
    }
}
