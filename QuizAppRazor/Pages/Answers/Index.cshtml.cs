using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Answers
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Answer> Answers { get; set; } = new();
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public int QuizId { get; set; }

        public async Task<IActionResult> OnGetAsync(int questionId)
        {
            var question = await _context.Questions
                .Include(q => q.Quiz)
                .FirstOrDefaultAsync(q => q.Id == questionId);

            if (question == null)
                return NotFound();

            QuestionId = questionId;
            QuestionText = question.Text;
            QuizId = question.QuizId;

            Answers = await _context.Answers
                .Where(a => a.QuestionId == questionId)
                .ToListAsync();

            return Page();
        }
    }
}
