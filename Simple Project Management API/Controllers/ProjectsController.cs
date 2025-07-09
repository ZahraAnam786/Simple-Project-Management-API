using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Models;
using Simple_Project_Management_API.Services.IServices;

namespace Simple_Project_Management_API.Controllers
{
    [Route("api/projects")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var projects = await _service.GetAllAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var project = await _service.GetByIdAsync(id);

            if (project == null) 
                return NotFound();

            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProjectCreateDto project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateAsync(project);

            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ProjectUpdateDto ProjectDto)
        {
            if (id != ProjectDto.Id) 
                return BadRequest();

            var updated = await _service.UpdateAsync(ProjectDto);

            if (updated == null) 
                return NotFound();

            return Ok(updated);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) 
                return NotFound();

            return NoContent();
        }
    }
}

