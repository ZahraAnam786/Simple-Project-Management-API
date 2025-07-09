using System.ComponentModel.DataAnnotations;

namespace Simple_Project_Management_API.DTOs
{
    public record ProjectTaskCreateDto(
        [Required, MaxLength(100)] string Title,
        bool IsCompleted,
        DateTime DueDate
    );
}
