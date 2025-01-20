using Microsoft.EntityFrameworkCore;
using GerenciadorDeTarefas.Domain.Entities;

namespace GerenciadorDeTarefas.Infrastructure.Data
{
    public class TarefaContexto : DbContext
    {
        public TarefaContexto(DbContextOptions<TarefaContexto> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TarefaContexto).Assembly);
        }
    }
}