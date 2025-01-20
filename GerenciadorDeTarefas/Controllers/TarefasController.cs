using Microsoft.AspNetCore.Mvc;
using GerenciadorDeTarefas.Application.DTOs;
using GerenciadorDeTarefas.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace GerenciadorDeTarefas.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TarefasController : ControllerBase
{
    private readonly ITarefaServico _tarefaServico;
    private readonly HashSet<string> _valoresPermitidosOrdenarPor = new HashSet<string> { "datavencimento", "titulo", "status" };

    public TarefasController(ITarefaServico tarefaServico)
    {
        _tarefaServico = tarefaServico;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetTarefas([FromQuery] string status, [FromQuery] string ordenarPor)
    {
        if (string.IsNullOrEmpty(ordenarPor) || !_valoresPermitidosOrdenarPor.Contains(ordenarPor.ToLower()))
        {
            return BadRequest("O campo 'ordenarPor' é obrigatório e deve ser 'datavencimento', 'titulo' ou 'status'.");
        }

        var tarefas = await _tarefaServico.ObterTarefasAsync(status, ordenarPor);
        return Ok(tarefas);
    }

    [HttpPost]
    public async Task<ActionResult<TarefaDTO>> PostTarefa(TarefaDTO tarefaDto)
    {
        await _tarefaServico.AdicionarTarefaAsync(tarefaDto);
        return CreatedAtAction(nameof(GetTarefas), new { id = tarefaDto.Id }, tarefaDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarefa(int id, TarefaDTO tarefaDto)
    {
        if (id != tarefaDto.Id)
        {
            return BadRequest();
        }

        await _tarefaServico.AtualizarTarefaAsync(tarefaDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTarefa(int id)
    {
        await _tarefaServico.RemoverTarefaAsync(id);
        return NoContent();
    }
}