using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Questions
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Question> Questions { get; set; } = new();
        public int QuizId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int quizId)
        {
            var quiz = await _context.Quizzes.FindAsync(quizId);
            if (quiz == null)
                return NotFound();

            QuizId = quizId;
            QuizTitle = quiz.Title;

            Questions = await _context.Questions
                .Where(q => q.QuizId == quizId)
                .ToListAsync();

            return Page();
        }
    }
}
