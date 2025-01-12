using System.ComponentModel.DataAnnotations;

namespace QuizApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa quizu jest wymagana.")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        // Relacja jeden-do-wielu (Quiz -> Questions)
        public ICollection<Question>? Questions { get; set; }
    }
}
