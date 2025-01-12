using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Quizzes
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Quiz Quiz { get; set; } = new Quiz();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            Quiz = quiz;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var quizToUpdate = await _context.Quizzes.FindAsync(id);

            if (quizToUpdate == null)
            {
                return NotFound();
            }

            quizToUpdate.Title = Quiz.Title;
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
