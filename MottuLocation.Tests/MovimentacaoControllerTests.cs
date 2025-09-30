using Xunit;
using Moq;
using MottuLocation.Controllers;
using MottuLocation.Services;
using MottuLocation.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MottuLocation.Exceptions;
using System.Collections.Generic;

public class MovimentacaoControllerTests
{
    private readonly Mock<IMovimentacaoService> _mockMovimentacaoService;
    private readonly MovimentacaoController _controller;

    public MovimentacaoControllerTests()
    {
        _mockMovimentacaoService = new Mock<IMovimentacaoService>();
        _controller = new MovimentacaoController(_mockMovimentacaoService.Object);

        // Mocking IUrlHelper for HATEOAS link generation
        var mockUrlHelper = new Mock<IUrlHelper>();
        mockUrlHelper.Setup(x => x.Link(It.IsAny<string>(), It.IsAny<object>())).Returns("http://localhost/fake-link");
        _controller.Url = mockUrlHelper.Object;
    }

    [Fact]
    public async Task ListarMovimentacoesPorMoto_WhenMotoNotFound_ShouldReturnNotFound()
    {
        // Arrange
        long nonExistentMotoId = 777;
        _mockMovimentacaoService.Setup(service => service.ListarMovimentacoesPorMotoAsync(nonExistentMotoId, 0, 10, "dataHora"))
            .ThrowsAsync(new ResourceNotFoundException("Moto não encontrada."));

        // Act
        var result = await _controller.ListarMovimentacoesPorMoto(nonExistentMotoId);

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<MovimentacaoDTO>>>(result);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult.Result);
        Assert.Equal("Moto não encontrada.", notFoundResult.Value);
    }
}