using Xunit;
using Moq;
using MottuLocation.Controllers;
using MottuLocation.Services;
using MottuLocation.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MottuLocation.Exceptions;

namespace MottuLocation.Tests
{
    public class MovimentacaoControllerTests
    {
        private readonly Mock<IMovimentacaoService> _mockMovimentacaoService;
        private readonly MovimentacaoController _controller;

        public MovimentacaoControllerTests()
        {
            _mockMovimentacaoService = new Mock<IMovimentacaoService>();
            _controller = new MovimentacaoController(_mockMovimentacaoService.Object);
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost");
            _controller.Url = mockUrlHelper.Object;
        }

        [Fact]
        public async Task RegistrarMovimentacao_ReturnsCreatedAtAction_WhenSuccessful()
        {
            // Arrange
            var request = new MovimentacaoRequest { Rfid = "123", SensorCodigo = "ABC" };
            var movimentacaoDTO = new MovimentacaoDTO { Id = 1, MotoId = 1, SensorId = 1 };
            _mockMovimentacaoService.Setup(s => s.RegistrarMovimentacaoAsync(request.Rfid, request.SensorCodigo))
                .ReturnsAsync(movimentacaoDTO);

            // Act
            var result = await _controller.RegistrarMovimentacao(request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(MovimentacaoController.ListarMovimentacoesPorMoto), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task RegistrarMovimentacao_ReturnsNotFound_WhenResourceNotFound()
        {
            // Arrange
            var request = new MovimentacaoRequest { Rfid = "123", SensorCodigo = "ABC" };
            _mockMovimentacaoService.Setup(s => s.RegistrarMovimentacaoAsync(request.Rfid, request.SensorCodigo))
                .ThrowsAsync(new ResourceNotFoundException("Resource not found"));

            // Act
            var result = await _controller.RegistrarMovimentacao(request);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}
