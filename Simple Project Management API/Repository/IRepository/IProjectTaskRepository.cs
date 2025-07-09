using Simple_Project_Management_API.Models;

namespace Simple_Project_Management_API.Repository.IRepository
{
    public interface IProjectTaskRepository
    {
        Task<ProjectTask?> GetByIdAsync(int id);
        Task<ProjectTask> AddAsync(ProjectTask task);
        Task<ProjectTask?> UpdateAsync(ProjectTask task);
        Task<bool> DeleteAsync(int id);
    }
}
