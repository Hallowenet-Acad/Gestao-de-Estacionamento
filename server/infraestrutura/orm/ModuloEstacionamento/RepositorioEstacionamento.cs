using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento;
using Gestao_de_Estacionamento.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.ModuloEstacionamento;

public class RepositorioEstacionamento(AppDbContext contexto)
    : RepositorioBaseORM<Estacionamento>(contexto), IRepositorioEstacionamento
{
    public override async Task<Estacionamento?> SelecionarRegistroPorIdAsync(Guid idRegistro)
    {
        return await registros.Where(v => v.Id.Equals(idRegistro))
            .Include(e => e.Vagas)
            .FirstOrDefaultAsync();
    }

    public async Task<Estacionamento?> SelecionarRegistroPorNomeAsync(string nome, Guid? usuarioId, CancellationToken ct = default)
    {
        return await registros.Where(v => v.Nome.Equals(nome) && v.UsuarioId.Equals(usuarioId))
            .Include(e => e.Vagas)
            .FirstOrDefaultAsync(ct);
    }
}
