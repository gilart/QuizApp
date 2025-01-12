using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizAppRazor.Pages.Play
{
    public class ResultModel : PageModel
    {
        public int Score { get; set; }
        public int Total { get; set; }

        public void OnGet()
        {
            Score = TempData["Score"] != null ? (int)TempData["Score"] : 0;
            Total = TempData["Total"] != null ? (int)TempData["Total"] : 0;
        }
    }
}
