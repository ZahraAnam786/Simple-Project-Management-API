using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple_Project_Management_API.Models
{
    public class ProjectTask
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime DueDate { get; set; }

        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
