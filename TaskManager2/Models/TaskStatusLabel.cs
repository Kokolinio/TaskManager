using System.ComponentModel.DataAnnotations;

namespace TaskManager2.Models
{
    public class TaskStatusLabel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
