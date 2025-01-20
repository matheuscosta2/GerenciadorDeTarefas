using GerenciadorDeTarefas.Domain.Entities;

namespace GerenciadorDeTarefas.Domain.Interfaces;

public interface ITarefaRepositorio
{
    Task<IEnumerable<Tarefa>> ObterTarefasAsync();
    Task<Tarefa> ObterTarefaPorIdAsync(int id);
    Task AdicionarTarefaAsync(Tarefa tarefa);
    Task AtualizarTarefaAsync(Tarefa tarefa);
    Task RemoverTarefaAsync(int id);
}