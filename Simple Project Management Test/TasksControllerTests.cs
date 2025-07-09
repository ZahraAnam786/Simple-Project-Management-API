using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Simple_Project_Management_API.Controllers;
using Simple_Project_Management_API.DTOs;
using Simple_Project_Management_API.Services.IServices;

namespace Simple_Project_Management_Test
{
    public class TasksControllerTests
    {
        private readonly Mock<IProjectTaskService> _mockService;
        private readonly TasksController _controller;

        public TasksControllerTests()
        {
            _mockService = new Mock<IProjectTaskService>();
            _controller = new TasksController(_mockService.Object);
        }

        [Fact]
        public async Task Post_ShouldReturnSuccess_WhenTaskCreated()
        {
            // Arrange
            var dto = new ProjectTaskCreateDto("Test Task", false, DateTime.Today);
            var createdDto = new ProjectTaskDto(1, "Test Task", false, DateTime.Today);

            _mockService.Setup(s => s.AddToProjectAsync(1, dto)).ReturnsAsync(createdDto);

            // Act
            var result = await _controller.Post(1, dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Result);
        }

        [Fact]
        public async Task Post_ShouldReturnFailure_WhenTaskCreationFails()
        {
            // Arrange
            var dto = new ProjectTaskCreateDto("Invalid Task", false, DateTime.Today);

            _mockService.Setup(s => s.AddToProjectAsync(999, dto)).ReturnsAsync((ProjectTaskDto?)null);

            // Act
            var result = await _controller.Post(999, dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Project not found", result.Message);
        }

        [Fact]
        public async Task Put_ShouldReturnSuccess_WhenTaskUpdated()
        {
            // Arrange
            var dto = new ProjectTaskUpdateDto(1, "Updated", true, DateTime.Today, 1);
            var updatedDto = new ProjectTaskDto(1, "Updated", true, DateTime.Today);

            _mockService.Setup(s => s.UpdateAsync(dto)).ReturnsAsync(updatedDto);

            // Act
            var result = await _controller.Put(1, dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Updated", ((ProjectTaskDto)result.Result).Title);
        }

        [Fact]
        public async Task Put_ShouldReturnFailure_WhenIdMismatch()
        {
            var dto = new ProjectTaskUpdateDto(5, "Updated", true, DateTime.Today, 1);

            var result = await _controller.Put(1, dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Not found", result.Message);
        }

        [Fact]
        public async Task Delete_ShouldReturnSuccess_WhenTaskDeleted()
        {
            _mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

            var result = await _controller.Delete(1);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Delete_ShouldReturnFailure_WhenTaskNotFound()
        {
            _mockService.Setup(s => s.DeleteAsync(999)).ReturnsAsync(false);

            var result = await _controller.Delete(999);

            Assert.False(result.IsSuccess);
            Assert.Equal("Not found", result.Message);
        }
    }
}
