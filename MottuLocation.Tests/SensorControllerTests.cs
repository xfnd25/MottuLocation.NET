using Xunit;
using Moq;
using MottuLocation.Controllers;
using MottuLocation.Services;
using MottuLocation.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MottuLocation.Tests
{
    public class SensorControllerTests
    {
        private readonly Mock<ISensorService> _mockSensorService;
        private readonly SensorController _controller;

        public SensorControllerTests()
        {
            _mockSensorService = new Mock<ISensorService>();
            _controller = new SensorController(_mockSensorService.Object);
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost");
            _controller.Url = mockUrlHelper.Object;
        }

        [Fact]
        public async Task GetSensorById_ReturnsOkResult_WithSensor()
        {
            // Arrange
            var sensorId = 1;
            var sensorDTO = new SensorDTO { Id = sensorId, Codigo = "ABC" };
            _mockSensorService.Setup(s => s.GetSensorByIdAsync(sensorId)).ReturnsAsync(sensorDTO);

            // Act
            var result = await _controller.GetSensorById(sensorId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedSensor = Assert.IsType<SensorDTO>(okResult.Value);
            Assert.Equal(sensorId, returnedSensor.Id);
        }

        [Fact]
        public async Task GetSensorById_ReturnsNotFound_WhenSensorDoesNotExist()
        {
            // Arrange
            var sensorId = 1;
            _mockSensorService.Setup(s => s.GetSensorByIdAsync(sensorId)).ReturnsAsync((SensorDTO)null);

            // Act
            var result = await _controller.GetSensorById(sensorId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
