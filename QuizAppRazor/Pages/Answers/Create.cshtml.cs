using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Answers
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Answer Answer { get; set; } = new();
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(int questionId)
        {
            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
                return NotFound();

            QuestionId = questionId;
            QuestionText = question.Text;

            Answer.QuestionId = questionId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int questionId)
        {
            if (!ModelState.IsValid)
                return Page();

            var question = await _context.Questions.FindAsync(questionId);
            if (question == null)
                return NotFound();

            Answer.QuestionId = questionId;
            _context.Answers.Add(Answer);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index", new { questionId });
        }
    }
}
