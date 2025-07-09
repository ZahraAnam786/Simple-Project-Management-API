using Simple_Project_Management_API.Models;

namespace Simple_Project_Management_API.Repository.IRepository
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(int id);
        Task<Project> AddAsync(Project project);
        Task<Project?> UpdateAsync(Project project);
        Task<bool> DeleteAsync(int id);
    }
}
