namespace QuizAppRazor.Models.ViewModels
{
    public class QuestionCreateViewModel
    {
        public string Text { get; set; } = string.Empty;
        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel> 
        {
            new AnswerViewModel(),
            new AnswerViewModel()
        };
    }
}
