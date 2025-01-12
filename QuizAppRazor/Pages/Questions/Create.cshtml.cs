using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizAppRazor.Data;
using QuizAppRazor.Models;
using QuizAppRazor.Models.ViewModels;

namespace QuizAppRazor.Pages.Questions
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QuestionCreateViewModel QuestionVM { get; set; } = new();
        public int QuizId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;

        public void OnGet(int quizId)
        {
            QuizId = quizId;
        }

        public IActionResult OnPostAddAnswer()
        {
            QuestionVM.Answers.Add(new AnswerViewModel());
            return Page();
        }

        public IActionResult OnPostDelete(int index)
        {
            if (QuestionVM.Answers == null || index < 0 || index >= QuestionVM.Answers.Count || QuestionVM.Answers.Count == 2)
            {
                return Page();
            }

            QuestionVM.Answers.RemoveAt(index);

            return Page();
        }

        public async Task<IActionResult> OnPostSave(int quizId)
        {
            if (!ModelState.IsValid)
                return Page();

            // Sprawdzenie, czy quiz istnieje
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null)
                return NotFound();

            // 1) Tworzymy obiekt Question
            var newQuestion = new Question
            {
                Text = QuestionVM.Text,
                QuizId = quizId
            };


            newQuestion.Answers = QuestionVM.Answers
                .Where(q => !string.IsNullOrWhiteSpace(q.Text))
                .Select(q => new Answer
                {
                    Text = q.Text,
                    IsCorrect = q.IsCorrect,
                }).ToList();

            _context.Questions.Add(newQuestion);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Quizzes/Details", new { id = quizId });
        }
    }
}
