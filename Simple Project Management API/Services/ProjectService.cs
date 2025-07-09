using AutoMapper;
using Simple_Project_Management_API.Data;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;
using Simple_Project_Management_API.Repository.IRepository;
using Simple_Project_Management_API.Services.IServices;

namespace Simple_Project_Management_API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetByIdAsync(int id)
        {
            var project = await _repository.GetByIdAsync(id);
            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateAsync(ProjectCreateDto dto)
        {
            var entity = _mapper.Map<Project>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<ProjectDto>(created);
        }

        public async Task<ProjectDto?> UpdateAsync(ProjectUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(dto.Id);
            if (existing == null) return null;

            // Map updates onto existing entity
            _mapper.Map(dto, existing);
            var updated = await _repository.UpdateAsync(existing);

            return updated == null ? null : _mapper.Map<ProjectDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}