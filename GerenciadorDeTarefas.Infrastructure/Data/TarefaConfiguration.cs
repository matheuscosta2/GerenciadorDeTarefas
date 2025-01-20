using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GerenciadorDeTarefas.Domain.Entities;

namespace GerenciadorDeTarefas.Infrastructure.Configurations;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Titulo).IsRequired().HasMaxLength(100);
        builder.Property(t => t.DataVencimento).IsRequired();
        builder.Property(t => t.Status).IsRequired();
    }
}