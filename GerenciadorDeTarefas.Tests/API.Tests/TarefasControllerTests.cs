using Moq;
using Microsoft.AspNetCore.Mvc;
using GerenciadorDeTarefas.API.Controllers;
using GerenciadorDeTarefas.Application.Interfaces;
using GerenciadorDeTarefas.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GerenciadorDeTarefas.Tests.API.Tests;

public class TarefasControllerTests
{
    private readonly Mock<ITarefaServico> _tarefaServicoMock;
    private readonly TarefasController _tarefasController;

    public TarefasControllerTests()
    {
        _tarefaServicoMock = new Mock<ITarefaServico>();
        _tarefasController = new TarefasController(_tarefaServicoMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
       {
            new Claim(ClaimTypes.Name, "UsuarioTeste")
       }, "mock"));

        _tarefasController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Fact]
    public async Task GetTarefas_ShouldReturnOk_WhenTarefasExist()
    {
        // Arrange
        var tarefas = new List<TarefaDTO> { new TarefaDTO { Id = 1, Titulo = "Teste" } };
        _tarefaServicoMock.Setup(serv => serv.ObterTarefasAsync(It.IsAny<string>(), It.IsAny<string>()))
                          .ReturnsAsync(tarefas);

        // Act
        var result = await _tarefasController.GetTarefas(null, "titulo");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<TarefaDTO>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal("Teste", returnValue[0].Titulo);
    }

    [Fact]
    public async Task GetTarefas_ShouldReturnBadRequest_WhenOrdenarPorIsInvalid()
    {
        // Act
        var result = await _tarefasController.GetTarefas(null, "invalid");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal("O campo 'ordenarPor' é obrigatório e deve ser 'datavencimento', 'titulo' ou 'status'.", badRequestResult.Value);
    }

    [Fact]
    public async Task PostTarefa_ShouldReturnCreatedAtAction_WhenTarefaIsAdded()
    {
        // Arrange
        var tarefaDto = new TarefaDTO { Id = 1, Titulo = "Teste" };
        _tarefaServicoMock.Setup(serv => serv.AdicionarTarefaAsync(It.IsAny<TarefaDTO>())).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefasController.PostTarefa(tarefaDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(_tarefasController.GetTarefas), createdAtActionResult.ActionName);
        Assert.Equal(tarefaDto.Id, ((TarefaDTO)createdAtActionResult.Value).Id);
    }

    [Fact]
    public async Task PutTarefa_ShouldReturnNoContent_WhenTarefaIsUpdated()
    {
        // Arrange
        var tarefaDto = new TarefaDTO { Id = 1, Titulo = "Teste Atualizado" };
        _tarefaServicoMock.Setup(serv => serv.AtualizarTarefaAsync(It.IsAny<TarefaDTO>())).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefasController.PutTarefa(1, tarefaDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutTarefa_ShouldReturnBadRequest_WhenIdDoesNotMatch()
    {
        // Arrange
        var tarefaDto = new TarefaDTO { Id = 1, Titulo = "Teste Atualizado" };

        // Act
        var result = await _tarefasController.PutTarefa(2, tarefaDto);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task DeleteTarefa_ShouldReturnNoContent_WhenTarefaIsRemoved()
    {
        // Arrange
        var tarefaId = 1;
        _tarefaServicoMock.Setup(serv => serv.RemoverTarefaAsync(tarefaId)).Returns(Task.CompletedTask);

        // Act
        var result = await _tarefasController.DeleteTarefa(tarefaId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}