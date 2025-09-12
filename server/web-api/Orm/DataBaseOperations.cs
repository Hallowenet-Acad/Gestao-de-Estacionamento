using System;
using Gestao_de_Estacionamento.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Estacionamento_web_api.Orm;

public static class DataBaseOperations
{
    public static void ApplyMigrations(this IHost app)
    {
        IServiceScope scope = app.Services.CreateScope();
        AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.Migrate();
    }
}
