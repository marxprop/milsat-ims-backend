using System.ComponentModel.DataAnnotations;

namespace MilsatIMS.Models
{
    public class Prompt
    {
        [Key]
        public Guid PromptId { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public DateTime PublishDate { get; set; } = DateTime.UtcNow;
    }
}
