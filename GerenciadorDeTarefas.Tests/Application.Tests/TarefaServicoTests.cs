using Moq;
using GerenciadorDeTarefas.Application.Services;
using GerenciadorDeTarefas.Domain.Interfaces;
using GerenciadorDeTarefas.Application.DTOs;
using AutoMapper;
using GerenciadorDeTarefas.Domain.Entities;

namespace GerenciadorDeTarefas.Tests.Application.Tests;

public class TarefaServicoTests
{
    private readonly Mock<ITarefaRepositorio> _tarefaRepositorioMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly TarefaServico _tarefaServico;

    public TarefaServicoTests()
    {
        _tarefaRepositorioMock = new Mock<ITarefaRepositorio>();
        _mapperMock = new Mock<IMapper>();
        _tarefaServico = new TarefaServico(_tarefaRepositorioMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task ObterTarefasAsync_ShouldReturnTarefas_WhenTarefasExist()
    {
        // Arrange
        var tarefas = new List<Tarefa> { new Tarefa { Id = 1, Titulo = "Teste" } };
        _tarefaRepositorioMock.Setup(repo => repo.ObterTarefasAsync()).ReturnsAsync(tarefas);
        _mapperMock.Setup(m => m.Map<IEnumerable<TarefaDTO>>(It.IsAny<IEnumerable<Tarefa>>()))
                   .Returns(tarefas.Select(t => new TarefaDTO { Id = t.Id, Titulo = t.Titulo }));

        // Act
        var result = await _tarefaServico.ObterTarefasAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Teste", result.First().Titulo);
    }

    [Fact]
    public async Task ObterTarefasAsync_ShouldReturnFilteredTarefas_WhenStatusIsProvided()
    {
        // Arrange
        var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, Titulo = "Teste 1", Status = "Pendente" },
            new Tarefa { Id = 2, Titulo = "Teste 2", Status = "Concluído" }
        };
        _tarefaRepositorioMock.Setup(repo => repo.ObterTarefasAsync()).ReturnsAsync(tarefas);
        _mapperMock.Setup(m => m.Map<IEnumerable<TarefaDTO>>(It.IsAny<IEnumerable<Tarefa>>()))
                   .Returns(tarefas.Where(t => t.Status == "Pendente").Select(t => new TarefaDTO { Id = t.Id, Titulo = t.Titulo, Status = t.Status }));

        // Act
        var result = await _tarefaServico.ObterTarefasAsync("Pendente");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("Pendente", result.First().Status);
    }

    [Fact]
    public async Task ObterTarefasAsync_ShouldReturnOrderedTarefas_WhenOrdenarPorIsProvided()
    {
        // Arrange
        var tarefas = new List<Tarefa>
        {
            new Tarefa { Id = 1, Titulo = "B" },
            new Tarefa { Id = 2, Titulo = "A" }
        };
        _tarefaRepositorioMock.Setup(repo => repo.ObterTarefasAsync()).ReturnsAsync(tarefas);
        _mapperMock.Setup(m => m.Map<IEnumerable<TarefaDTO>>(It.IsAny<IEnumerable<Tarefa>>()))
                   .Returns(tarefas.OrderBy(t => t.Titulo).Select(t => new TarefaDTO { Id = t.Id, Titulo = t.Titulo }));

        // Act
        var result = await _tarefaServico.ObterTarefasAsync(null, "titulo");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("A", result.First().Titulo);
    }

    [Fact]
    public async Task AdicionarTarefaAsync_ShouldCallRepositorio_WhenTarefaIsAdded()
    {
        // Arrange
        var tarefaDto = new TarefaDTO { Id = 1, Titulo = "Teste" };
        var tarefa = new Tarefa { Id = 1, Titulo = "Teste" };
        _mapperMock.Setup(m => m.Map<Tarefa>(It.IsAny<TarefaDTO>())).Returns(tarefa);

        // Act
        await _tarefaServico.AdicionarTarefaAsync(tarefaDto);

        // Assert
        _tarefaRepositorioMock.Verify(repo => repo.AdicionarTarefaAsync(It.IsAny<Tarefa>()), Times.Once);
    }

    [Fact]
    public async Task AtualizarTarefaAsync_ShouldCallRepositorio_WhenTarefaIsUpdated()
    {
        // Arrange
        var tarefaDto = new TarefaDTO { Id = 1, Titulo = "Teste Atualizado" };
        var tarefa = new Tarefa { Id = 1, Titulo = "Teste Atualizado" };
        _mapperMock.Setup(m => m.Map<Tarefa>(It.IsAny<TarefaDTO>())).Returns(tarefa);

        // Act
        await _tarefaServico.AtualizarTarefaAsync(tarefaDto);

        // Assert
        _tarefaRepositorioMock.Verify(repo => repo.AtualizarTarefaAsync(It.IsAny<Tarefa>()), Times.Once);
    }

    [Fact]
    public async Task RemoverTarefaAsync_ShouldCallRepositorio_WhenTarefaIsRemoved()
    {
        // Arrange
        var tarefaId = 1;

        // Act
        await _tarefaServico.RemoverTarefaAsync(tarefaId);

        // Assert
        _tarefaRepositorioMock.Verify(repo => repo.RemoverTarefaAsync(tarefaId), Times.Once);
    }
}