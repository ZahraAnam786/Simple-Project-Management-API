using Simple_Project_Management_API.DTOs;

namespace Simple_Project_Management_API.Services.IServices
{
    public interface IProjectTaskService
    {
        Task<ProjectTaskDto?> AddToProjectAsync(int projectId, ProjectTaskCreateDto dto);
        Task<ProjectTaskDto?> UpdateAsync(ProjectTaskUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
