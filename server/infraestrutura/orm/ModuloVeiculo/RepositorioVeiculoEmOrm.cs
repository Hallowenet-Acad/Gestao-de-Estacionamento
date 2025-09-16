using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using Gestao_de_Estacionamento.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.ModuloVeiculo;

public class RepositorioVeiculoEmOrm : RepositorioBaseORM<Veiculo>, IRepositorioVeiculo
{
    public RepositorioVeiculoEmOrm(AppDbContext contexto) : base(contexto) { }

    public override async Task<Veiculo?> SelecionarRegistroPorIdAsync(Guid idRegistro)
    {
        return registros 
            .Include(v => v.Modelo)
            .FirstOrDefault(v => v.Id.Equals(idRegistro));
    }

    public override async Task<List<Veiculo>> SelecionarRegistrosAsync()
    {
        return registros
            .Include(f => f.Modelo)
            .ToList();
    }
}
