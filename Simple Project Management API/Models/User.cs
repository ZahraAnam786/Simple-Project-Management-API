using System.ComponentModel.DataAnnotations;

namespace Simple_Project_Management_API.Models
{
    public class User
    {
        [Key]
        public long UserID { get; set; }

        [Required, MaxLength(100)]
        public string UserName { get; set; }

        [Required, MaxLength(50)]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required, MaxLength(20)]
        public string? Password { get; set; }
    }
}
