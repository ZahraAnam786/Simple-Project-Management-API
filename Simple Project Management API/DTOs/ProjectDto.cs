namespace Simple_Project_Management_API.DTOs
{

    public record ProjectDto
    (
        int Id,
        string Name,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate,
        List<ProjectTaskDto> Tasks
    );

}
