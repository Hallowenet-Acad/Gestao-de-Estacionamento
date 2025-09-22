using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento.ModuloVaga;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.Compartilhado;

public class AppDbContext(DbContextOptions options, ITenantProvider? tenantProvider = null) : IdentityDbContext<Usuario, Cargo, Guid>(options), IUnitOfWork
{
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Vaga> Vagas { get; set; }

    private readonly ITenantProvider? tenantProvider;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (tenantProvider is not null)
        {
            modelBuilder.Entity<Veiculo>()
                .HasQueryFilter(x => x.UsuarioId.Equals(tenantProvider.UsuarioId));
        }

        var assembly = typeof(AppDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        base.OnModelCreating(modelBuilder);
    }
    public async Task CommitAsync()
    {
        await SaveChangesAsync();
    }

    public async Task RollbackAsync()
    {
        foreach (EntityEntry entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;
                    break;
            }
        }

        await Task.CompletedTask;
    }
}
