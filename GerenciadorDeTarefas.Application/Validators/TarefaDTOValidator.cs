using FluentValidation;
using GerenciadorDeTarefas.Application.DTOs;

namespace GerenciadorDeTarefas.Application.Validators;

public class TarefaDTOValidator : AbstractValidator<TarefaDTO>
{
    public TarefaDTOValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("Título é obrigatório.")
            .Length(3, 100).WithMessage("Título deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.DataVencimento)
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Data de vencimento não pode ser anterior à data atual.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status é obrigatório.")
            .Must(status => status == "Pendente" || status == "Em andamento" || status == "Concluído")
            .WithMessage("Status deve ser 'Pendente', 'Em andamento' ou 'Concluído'.");
    }
}