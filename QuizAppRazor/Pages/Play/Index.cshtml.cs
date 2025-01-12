using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Play
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public Quiz Quiz { get; set; } = new();
        [BindProperty]
        public int QuizId { get; set; }

        // Klasa pomocnicza do bindowania odpowiedzi
        [BindProperty]
        public List<QuestionAnswerVM> Questions { get; set; } = new();

        public class QuestionAnswerVM
        {
            public int QuestionId { get; set; }
            public List<int> SelectedAnswers { get; set; } = new();
        }

        public async Task<IActionResult> OnGetAsync(int quizId)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == quizId);

            if (quiz == null)
                return NotFound();

            Quiz = quiz;
            QuizId = quiz.Id;

            // Przygotuj pust¹ listê do bindowania
            if (quiz.Questions != null)
            {
                foreach (var q in quiz.Questions)
                {
                    Questions.Add(new QuestionAnswerVM
                    {
                        QuestionId = q.Id
                    });
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Odczyt quizu z bazy (z odpowiedziami)
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == QuizId);

            if (quiz == null)
                return NotFound();

            // Liczymy punkty
            int totalQuestions = quiz.Questions?.Count ?? 0;
            int correctCount = 0; // ile pytañ w 100% poprawnie zaznaczono

            if (quiz.Questions != null)
            {
                // Tworzymy mapê: questionId -> list<correctAnswerId>
                var correctAnswersMap = quiz.Questions
                    .ToDictionary(
                        q => q.Id,
                        q => q.Answers
                             .Where(a => a.IsCorrect)
                             .Select(a => a.Id)
                             .OrderBy(id => id)
                             .ToList()
                    );

                // Porównujemy z tym, co user zaznaczy³
                foreach (var questionVM in Questions)
                {
                    var correctAnsIds = correctAnswersMap[questionVM.QuestionId];
                    var userAns = questionVM.SelectedAnswers.OrderBy(id => id).ToList();

                    // Warunek: user musi zaznaczyæ dok³adnie te same IDs co w correctAnsIds
                    if (correctAnsIds.SequenceEqual(userAns))
                    {
                        correctCount++;
                    }
                }
            }

            // Przekazujemy wynik do innej strony (Result)
            TempData["Score"] = correctCount;
            TempData["Total"] = totalQuestions;

            return RedirectToPage("Result");
        }
    }
}
