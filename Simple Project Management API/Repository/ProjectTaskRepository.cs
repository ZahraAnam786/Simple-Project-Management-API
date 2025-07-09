using Simple_Project_Management_API.Data;
using Simple_Project_Management_API.Models;
using Simple_Project_Management_API.Repository.IRepository;

namespace Simple_Project_Management_API.Repository
{
    public class ProjectTaskRepository : IProjectTaskRepository
    {
        private readonly AppDBContext _context;

        public ProjectTaskRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask?> GetByIdAsync(int id)
        {
            return await _context.ProjectTasks.FindAsync(id);
        }

        public async Task<ProjectTask> AddAsync(ProjectTask task)
        {
            _context.ProjectTasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<ProjectTask?> UpdateAsync(ProjectTask task)
        {
            var existing = await _context.ProjectTasks.FindAsync(task.Id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(task);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return false;

            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
