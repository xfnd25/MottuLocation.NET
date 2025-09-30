using Xunit;
using Moq;
using MottuLocation.Controllers;
using MottuLocation.Services;
using MottuLocation.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class SensorControllerTests
{
    private readonly Mock<ISensorService> _mockSensorService;
    private readonly SensorController _controller;

    public SensorControllerTests()
    {
        _mockSensorService = new Mock<ISensorService>();
        _controller = new SensorController(_mockSensorService.Object);

        // Mocking IUrlHelper for HATEOAS link generation
        var mockUrlHelper = new Mock<IUrlHelper>();
        mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost/fake-link");
        _controller.Url = mockUrlHelper.Object;
    }

    [Fact]
    public async Task GetSensorById_WhenSensorDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        long nonExistentId = 888;
        _mockSensorService.Setup(service => service.GetSensorByIdAsync(nonExistentId))
            .ReturnsAsync((SensorDTO)null);

        // Act
        var result = await _controller.GetSensorById(nonExistentId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}