using Microsoft.EntityFrameworkCore;
using Simple_Project_Management_API.Data;
using Simple_Project_Management_API.Models;
using Simple_Project_Management_API.Repository.IRepository;

namespace Simple_Project_Management_API.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDBContext _context;

        public ProjectRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
            => await _context.Projects.Include(p => p.Tasks).ToListAsync();

        public async Task<Project?> GetByIdAsync(int id)
        {
            return await _context.Projects.Include(p => p.Tasks).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Project> AddAsync(Project project)
        {
            _context.Projects.Add(project);

            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> UpdateAsync(Project project)
        {
            var existing = await _context.Projects.FindAsync(project.Id);
            if (existing == null) 
                return null;

            _context.Entry(existing).CurrentValues.SetValues(project);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
