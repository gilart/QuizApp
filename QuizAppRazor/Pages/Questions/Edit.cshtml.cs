using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuizAppRazor.Data;
using QuizAppRazor.Models;
using QuizAppRazor.Models.ViewModels;

namespace QuizAppRazor.Pages.Questions
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public QuestionCreateViewModel Question { get; set; } = new QuestionCreateViewModel();
        [BindProperty]
        public int QuizId { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var question = await _context.Questions
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            Question = new QuestionCreateViewModel
            {
                Text = question.Text,
                Answers = question.Answers.Select(a => new AnswerViewModel
                {
                    Text = a.Text,
                    IsCorrect = a.IsCorrect
                }).ToList()
            };

            QuizId = question.QuizId;

            return Page();
        }

        public IActionResult OnPostAddAnswer()
        {
            Question.Answers.Add(new AnswerViewModel());

            return Page();
        }

        public IActionResult OnPostDelete(int index)
        {
            if (Question.Answers == null || index < 0 || index >= Question.Answers.Count || Question.Answers.Count == 2)
            {
                return Page();
            }

            Question.Answers.RemoveAt(index);

            return Page();
        }

        public async Task<IActionResult> OnPostSave(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var questionToUpdate = await _context.Questions
                .Include(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (questionToUpdate == null)
            {
                return NotFound();
            }

            questionToUpdate.Text = Question.Text;
            questionToUpdate.Answers = Question.Answers.Select(a => new Answer
            {
                Text = a.Text,
                IsCorrect = a.IsCorrect
            }).ToList();

            await _context.SaveChangesAsync();

            return RedirectToPage("Index", new { questionToUpdate.QuizId });
        }
    }
}
