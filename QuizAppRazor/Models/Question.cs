using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuizAppRazor.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Treść pytania jest wymagana.")]
        public string Text { get; set; } = string.Empty;

        // Pole do powiązania z Quiz
        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz? Quiz { get; set; }

        // Relacja: jedno pytanie -> wiele odpowiedzi
        public ICollection<Answer>? Answers { get; set; }
    }
}
