using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace TaskManager2.Models
{
    public class WorkTask
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        public string Description { get; set; }
        public int TaskStatusLabelId { get; set; }
        public virtual TaskStatusLabel? TaskStatusLabel { get; set; }
        public DateTime ExecutionDate { get; set; }
        public string? UserId { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}
