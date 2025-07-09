using AutoMapper;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;

namespace Simple_Project_Management_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Project mappings
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectCreateDto, Project>();
            CreateMap<ProjectUpdateDto, Project>();

            // Task mappings
            CreateMap<ProjectTask, ProjectTaskDto>();
            CreateMap<ProjectTaskCreateDto, ProjectTask>();
            CreateMap<ProjectTaskUpdateDto, ProjectTask>();


            //User
            CreateMap<RegisterDTO, User>();
            CreateMap<LoginDTO, User>();
        }
    }

}