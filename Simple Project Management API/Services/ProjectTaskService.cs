using AutoMapper;
using Simple_Project_Management_API.Data;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;
using Simple_Project_Management_API.Repository.IRepository;
using Simple_Project_Management_API.Services.IServices;

namespace Simple_Project_Management_API.Services
{
    public class ProjectTaskService : IProjectTaskService
    {
        private readonly IProjectTaskRepository _taskRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectTaskService(
            IProjectTaskRepository taskRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _taskRepository = taskRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectTaskDto?> AddToProjectAsync(int projectId, ProjectTaskCreateDto dto)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null) return null;

            var task = _mapper.Map<ProjectTask>(dto);
            task.ProjectId = projectId;

            var createdTask = await _taskRepository.AddAsync(task);
            return _mapper.Map<ProjectTaskDto>(createdTask);
        }

        public async Task<ProjectTaskDto?> UpdateAsync(ProjectTaskUpdateDto taskDto)
        {
            var task = await _taskRepository.GetByIdAsync(taskDto.Id);
            if (task == null) return null;

            _mapper.Map(taskDto, task); 
            
            // updates fields on the existing task
            var updatedTask = await _taskRepository.UpdateAsync(task);

            return updatedTask == null ? null : _mapper.Map<ProjectTaskDto>(updatedTask);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _taskRepository.DeleteAsync(id);
        }
    }

}