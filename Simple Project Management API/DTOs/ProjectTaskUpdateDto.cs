using System.ComponentModel.DataAnnotations;

namespace Simple_Project_Management_API.DTOs
{
    public record ProjectTaskUpdateDto(
        [Required] int Id,
        [Required, MaxLength(100)] string Title,
        bool IsCompleted,
        DateTime DueDate,
        int ProjectId
    );

}
