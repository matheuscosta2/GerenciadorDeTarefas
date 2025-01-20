using AutoMapper;
using GerenciadorDeTarefas.Application.DTOs;
using GerenciadorDeTarefas.Application.Interfaces;
using GerenciadorDeTarefas.Domain.Entities;
using GerenciadorDeTarefas.Domain.Interfaces;

namespace GerenciadorDeTarefas.Application.Services;

public class TarefaServico : ITarefaServico
{
    private readonly ITarefaRepositorio _tarefaRepositorio;
    private readonly IMapper _mapper;

    public TarefaServico(ITarefaRepositorio tarefaRepositorio, IMapper mapper)
    {
        _tarefaRepositorio = tarefaRepositorio;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TarefaDTO>> ObterTarefasAsync(string status = null, string ordenarPor = null)
    {
        var tarefas = await _tarefaRepositorio.ObterTarefasAsync();

        if (!string.IsNullOrEmpty(status))
        {
            tarefas = tarefas.Where(t => t.Status == status);
        }

        if (!string.IsNullOrEmpty(ordenarPor))
        {
            tarefas = ordenarPor.ToLower() switch
            {
                "datavencimento" => tarefas.OrderBy(t => t.DataVencimento),
                "titulo" => tarefas.OrderBy(t => t.Titulo),
                "status" => tarefas.OrderBy(t => t.Status),
                _ => tarefas
            };
        }

        return _mapper.Map<IEnumerable<TarefaDTO>>(tarefas);
    }

    public async Task<TarefaDTO> ObterTarefaPorIdAsync(int id)
    {
        var tarefa = await _tarefaRepositorio.ObterTarefaPorIdAsync(id);
        return _mapper.Map<TarefaDTO>(tarefa);
    }

    public async Task AdicionarTarefaAsync(TarefaDTO tarefaDto)
    {
        var tarefa = _mapper.Map<Tarefa>(tarefaDto);
        await _tarefaRepositorio.AdicionarTarefaAsync(tarefa);
    }

    public async Task AtualizarTarefaAsync(TarefaDTO tarefaDto)
    {
        var tarefa = _mapper.Map<Tarefa>(tarefaDto);
        await _tarefaRepositorio.AtualizarTarefaAsync(tarefa);
    }

    public async Task RemoverTarefaAsync(int id)
    {
        await _tarefaRepositorio.RemoverTarefaAsync(id);
    }
}