using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstDemo.Models
{
    public class Exame
    {

        [Key]
        public int ExameId { get; set; }
       public string ExameName { get; set; }
        public int Duration { get; set; }
        public DateTime ExameDate { get; set; }
        public int FullMark {  get; set; }

        public int CrsId { get; set; }
        [ForeignKey("CrsId")]
        public virtual Course Course { get; set; }
        public virtual ICollection<Question> Questions { get; set; } = new HashSet<Question>();
    }
}
