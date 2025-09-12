using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.Compartilhado;

public static class AppDbContextFactory
{
    public static AppDbContext CriarDbContext(string connectionString)
    {
        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new(options);
    }
}
