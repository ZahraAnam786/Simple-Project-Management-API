using System.ComponentModel.DataAnnotations;

namespace Simple_Project_Management_API.DTOs
{
    public record ProjectCreateDto(
        [Required, MaxLength(100)] string Name,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate
    );
}
