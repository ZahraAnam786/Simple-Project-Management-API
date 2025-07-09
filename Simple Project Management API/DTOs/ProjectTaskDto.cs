namespace Simple_Project_Management_API.DTOs
{
    public record ProjectTaskDto(
        int Id,
        string Title,
        bool IsCompleted,
        DateTime DueDate
    );
}
