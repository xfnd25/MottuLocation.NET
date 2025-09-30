using Xunit;
using Moq;
using MottuLocation.Controllers;
using MottuLocation.Services;
using MottuLocation.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class MotoControllerTests
{
    private readonly Mock<IMotoService> _mockMotoService;
    private readonly MotoController _controller;

    public MotoControllerTests()
    {
        _mockMotoService = new Mock<IMotoService>();
        _controller = new MotoController(_mockMotoService.Object);

        // Mocking IUrlHelper for HATEOAS link generation
        var mockUrlHelper = new Mock<IUrlHelper>();
        mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost/fake-link");
        _controller.Url = mockUrlHelper.Object;
    }

    [Fact]
    public async Task GetMotoById_WhenMotoDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        long nonExistentId = 999;
        _mockMotoService.Setup(service => service.GetMotoByIdAsync(nonExistentId))
            .ReturnsAsync((MotoDTO)null);

        // Act
        var result = await _controller.GetMotoById(nonExistentId);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task CreateMoto_WithValidModel_ShouldReturnCreatedAtAction()
    {
        // Arrange
        var motoDTO = new MotoDTO { Placa = "TTT-9999", Modelo = "Test Bike", Ano = 2024 };
        var createdMoto = new MotoDTO { Id = 1, Placa = "TTT-9999", Modelo = "Test Bike", Ano = 2024 };

        _mockMotoService.Setup(service => service.CreateMotoAsync(motoDTO))
            .ReturnsAsync(createdMoto);

        // Act
        var result = await _controller.CreateMoto(motoDTO);

        // Assert
        var actionResult = Assert.IsType<ActionResult<MotoDTO>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        Assert.Equal("GetMotoById", createdAtActionResult.ActionName);
        Assert.Equal(createdMoto.Id, ((MotoDTO)createdAtActionResult.Value).Id);
    }
}