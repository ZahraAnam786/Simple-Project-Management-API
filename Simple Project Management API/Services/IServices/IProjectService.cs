using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;

namespace Simple_Project_Management_API.Services.IServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(int id);
        Task<ProjectDto> CreateAsync(ProjectCreateDto dto);
        Task<ProjectDto?> UpdateAsync(ProjectUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
