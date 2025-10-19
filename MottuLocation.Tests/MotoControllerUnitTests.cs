using Xunit;
using Moq;
using MottuLocation.Controllers;
using MottuLocation.Services;
using MottuLocation.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MottuLocation.Tests
{
    public class MotoControllerUnitTests
    {
        private readonly Mock<IMotoService> _mockMotoService;
        private readonly MotoController _controller;

        public MotoControllerUnitTests()
        {
            _mockMotoService = new Mock<IMotoService>();
            _controller = new MotoController(_mockMotoService.Object);
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost");
            _controller.Url = mockUrlHelper.Object;
        }

        [Fact]
        public async Task GetMotoById_ReturnsOkResult_WithMoto()
        {
            // Arrange
            var motoId = 1;
            var motoDTO = new MotoDTO { Id = motoId, Placa = "ABC-1234" };
            _mockMotoService.Setup(s => s.GetMotoByIdAsync(motoId)).ReturnsAsync(motoDTO);

            // Act
            var result = await _controller.GetMotoById(motoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedMoto = Assert.IsType<MotoDTO>(okResult.Value);
            Assert.Equal(motoId, returnedMoto.Id);
        }

        [Fact]
        public async Task GetMotoById_ReturnsNotFound_WhenMotoDoesNotExist()
        {
            // Arrange
            var motoId = 1;
            _mockMotoService.Setup(s => s.GetMotoByIdAsync(motoId)).ReturnsAsync((MotoDTO)null);

            // Act
            var result = await _controller.GetMotoById(motoId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
