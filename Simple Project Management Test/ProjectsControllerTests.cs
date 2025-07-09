using Moq;
using Microsoft.AspNetCore.Mvc;
using Simple_Project_Management_API.Controllers;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Services.IServices;

namespace Simple_Project_Management_Test
{
    public class ProjectsControllerTests
    {
        private readonly Mock<IProjectService> _mockService;
        private readonly ProjectsController _controller;

        public ProjectsControllerTests()
        {
            _mockService = new Mock<IProjectService>();
            _controller = new ProjectsController(_mockService.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnAllProjects()
        {
            // Arrange
            var sampleProjects = new List<ProjectDto>
           {
                new ProjectDto(1, "Test 1", null, DateTime.Now, null, new List<ProjectTaskDto>()),
                new ProjectDto(2, "Test 2", null, DateTime.Now, null, new List<ProjectTaskDto>())
           };

            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(sampleProjects);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProjects = Assert.IsType<List<ProjectDto>>(okResult.Value);
            Assert.Equal(2, returnedProjects.Count);
        }

        [Fact]
        public async Task GetById_ShouldReturnProject_WhenExists()
        {
            // Arrange
            var project = new ProjectDto(1, "Sample Project", null, DateTime.Now, null, new());
            _mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(project);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<ProjectDto>(okResult.Value);
            Assert.Equal("Sample Project", returned.Name);
        }

        [Fact]
        public async Task GetById_ShouldReturnNotFound_WhenProjectMissing()
        {
            // Arrange
            _mockService.Setup(s => s.GetByIdAsync(999)).ReturnsAsync((ProjectDto?)null);

            // Act
            var result = await _controller.Get(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Post_ShouldReturnCreatedProject()
        {
            // Arrange
            var input = new ProjectCreateDto("New Project", null, DateTime.Now, null);
            var created = new ProjectDto(1, "New Project", null, DateTime.Now, null, new());

            _mockService.Setup(s => s.CreateAsync(input)).ReturnsAsync(created);

            // Act
            var result = await _controller.Post(input);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returned = Assert.IsType<ProjectDto>(createdResult.Value);
            Assert.Equal("New Project", returned.Name);
        }

        [Fact]
        public async Task Put_ShouldReturnUpdatedProject_WhenExists()
        {
            // Arrange
            var input = new ProjectUpdateDto(1, "Updated", null, DateTime.Now, null);
            var updated = new ProjectDto(1, "Updated", null, DateTime.Now, null, new());

            _mockService.Setup(s => s.UpdateAsync(input)).ReturnsAsync(updated);

            // Act
            var result = await _controller.Put(1, input);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<ProjectDto>(okResult.Value);
            Assert.Equal("Updated", returned.Name);
        }

        [Fact]
        public async Task Put_ShouldReturnNotFound_WhenProjectMissing()
        {
            var input = new ProjectUpdateDto(999, "Missing", null, DateTime.Now, null);
            _mockService.Setup(s => s.UpdateAsync(input)).ReturnsAsync((ProjectDto?)null);

            var result = await _controller.Put(999, input);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNoContent_WhenDeleted()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFound_WhenNotExists()
        {
            _mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

            var result = await _controller.Delete(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }

}
