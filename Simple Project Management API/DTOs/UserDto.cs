using System.ComponentModel.DataAnnotations;

namespace Simple_Project_Management_API.DTOs
{
    public record RegisterDTO(
        [Required, MaxLength(100)] string UserName,
        [Required, MaxLength(50)] string Email,
        [Required, MaxLength(20)] string Password);
    public record LoginDTO(
        [Required, MaxLength(50)] string Email,
        [Required, MaxLength(20)] string Password);
}
