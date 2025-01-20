using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefas.Domain.Entities;
using GerenciadorDeTarefas.Domain.Interfaces;

namespace GerenciadorDeTarefas.Infrastructure.Data;

public class TarefaRepositorio : ITarefaRepositorio
{
    private readonly TarefaContexto _contexto;

    public TarefaRepositorio(TarefaContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<IEnumerable<Tarefa>> ObterTarefasAsync()
    {
        return await _contexto.Tarefas.ToListAsync();
    }

    public async Task<Tarefa> ObterTarefaPorIdAsync(int id)
    {
        return await _contexto.Tarefas.FindAsync(id);
    }

    public async Task AdicionarTarefaAsync(Tarefa tarefa)
    {
        _contexto.Tarefas.Add(tarefa);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarTarefaAsync(Tarefa tarefa)
    {
        _contexto.Entry(tarefa).State = EntityState.Modified;
        await _contexto.SaveChangesAsync();
    }

    public async Task RemoverTarefaAsync(int id)
    {
        var tarefa = await _contexto.Tarefas.FindAsync(id);
        if (tarefa != null)
        {
            _contexto.Tarefas.Remove(tarefa);
            await _contexto.SaveChangesAsync();
        }
    }
}