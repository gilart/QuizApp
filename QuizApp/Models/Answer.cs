using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Treść odpowiedzi jest wymagana.")]
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
