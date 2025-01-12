using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizAppRazor.Data;
using QuizAppRazor.Models;
using QuizAppRazor.Models.ViewModels;

namespace QuizAppRazor.Pages.Quizzes
{
    public class CreateWithQuestionsModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateWithQuestionsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QuizCreateViewModel QuizVM { get; set; } = new QuizCreateViewModel();

        // Handler do KLIKNI�CIA "Dodaj pytanie"
        public IActionResult OnPostAddQuestion()
        {
            // Dodajemy puste pytanie do listy.
            QuizVM.Questions.Add(new QuestionInputModel());

            // Zwracamy t� sam� stron�, by user zobaczy� nowy input
            return Page();
        }

        public IActionResult OnPostAddAnswer(int id)
        {
            // Dodajemy puste pytanie do listy.
            QuizVM.Questions[id].Answers.Add(new TextInputModel());

            // Zwracamy t� sam� stron�, by user zobaczy� nowy input
            return Page();
        }

        public async Task<IActionResult> OnPostSave()
        {
            // Walidacja przy ostatecznym zapisie
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1) Tworzymy obiekt Quiz
            var newQuiz = new Quiz
            {
                Title = QuizVM.Title,
                Description = QuizVM.Description
            };

            // 2) Przerzucamy pytania (pomijamy te, kt�re s� puste)
            newQuiz.Questions = QuizVM.Questions
                .Where(q => !string.IsNullOrWhiteSpace(q.Text))
                .Select(q => new Question
                {
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        Text = a.Text
                    }).ToList()
                })
                .ToList();

            // 3) Zapis w bazie
            _context.Quizzes.Add(newQuiz);
            await _context.SaveChangesAsync();

            // 4) Przekierowanie np. do listy quiz�w
            return RedirectToPage("Index");
        }
    }
}
