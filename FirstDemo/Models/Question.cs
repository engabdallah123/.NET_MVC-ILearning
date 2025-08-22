using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public int ExameId { get; set; }
        [ForeignKey("ExameId")]
        public Exame Exame { get; set; }
        public virtual List<Option> Options { get; set; } = new();
    }
}
