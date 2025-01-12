using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;

namespace QuizAppRazor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;
        public List<Quiz> TopQuizzes { get; set; } = new();

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            TopQuizzes = _db.Quizzes
                            .OrderByDescending(q => q.Id)
                            .Take(3)
                            .Include(q => q.Questions)
                            .ToList();
        }
    }
}
