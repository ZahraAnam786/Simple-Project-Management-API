using System.ComponentModel.DataAnnotations;

namespace Simple_Project_Management_API.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public List<ProjectTask> Tasks { get; set; } = new();
    }
}
