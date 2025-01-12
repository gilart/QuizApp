using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;

namespace QuizAppRazor.Pages.Play
{
    public class PlayRandomModel : PageModel
    {
        private readonly AppDbContext _context;

        public PlayRandomModel(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Pobierz listê ID quizów z bazy
            var quizIds = await _context.Quizzes
                .Select(q => q.Id)
                .ToListAsync();

            if (quizIds.Count == 0)
            {
                // Gdy brak quizów w bazie
                return RedirectToPage("Index");
            }

            // Wylosuj jeden quiz
            var rand = new Random();
            int randomIndex = rand.Next(quizIds.Count); // losowy index
            int randomQuizId = quizIds[randomIndex];

            // Przekieruj do /Play/Index?quizId=...
            return RedirectToPage("Index", new { quizId = randomQuizId });
        }
    }
}
