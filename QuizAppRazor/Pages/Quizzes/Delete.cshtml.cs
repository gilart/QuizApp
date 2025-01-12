using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Quizzes
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz Quiz { get; set; } = new Quiz();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var quiz = await _context.Quizzes.FirstOrDefaultAsync(q => q.Id == id);
            if (quiz == null)
            {
                return NotFound();
            }
            Quiz = quiz;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
