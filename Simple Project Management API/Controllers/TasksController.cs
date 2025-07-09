using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;
using Simple_Project_Management_API.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Simple_Project_Management_API.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private readonly IProjectTaskService _service;
        private ResponseDto _response;

        public TasksController(IProjectTaskService service)
        {
            _service = service;
            _response = new ResponseDto();
        }

        [HttpPost("project/{projectId}")]
        public async Task<ResponseDto> Post(int projectId, ProjectTaskCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid) 
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Model State";
                }

                var task = await _service.AddToProjectAsync(projectId, dto);
                if (task == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Project not found";
                }
                else
                    _response.Result = task;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("tasks/{id}")]
        public async Task<ResponseDto> Put(int id, ProjectTaskUpdateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid Model State";
                }

                var updated = await _service.UpdateAsync(dto);

                if (updated == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                }
                else
                    _response.Result = updated;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
            
        }


        [HttpDelete("tasks/{id}")]
        public async Task<ResponseDto> Delete(int id)
        {
            try { 
                var deleted = await _service.DeleteAsync(id);
                if (!deleted)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Not found";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
