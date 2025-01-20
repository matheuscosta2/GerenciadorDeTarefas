using GerenciadorDeTarefas.Application.DTOs;

namespace GerenciadorDeTarefas.Application.Interfaces;

public interface ITarefaServico
{
    Task<IEnumerable<TarefaDTO>> ObterTarefasAsync(string status = null, string ordenarPor = null);
    Task<TarefaDTO> ObterTarefaPorIdAsync(int id);
    Task AdicionarTarefaAsync(TarefaDTO tarefaDto);
    Task AtualizarTarefaAsync(TarefaDTO tarefaDto);
    Task RemoverTarefaAsync(int id);
}