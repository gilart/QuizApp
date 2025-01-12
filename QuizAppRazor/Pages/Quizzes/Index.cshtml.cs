using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages.Quizzes
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();

        public async Task OnGetAsync()
        {
            Quizzes = await _context.Quizzes
                .Include(q => q.Questions)
                .ToListAsync();
        }
    }
}
