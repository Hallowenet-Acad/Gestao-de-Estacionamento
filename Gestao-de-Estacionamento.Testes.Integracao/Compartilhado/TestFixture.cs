using Gestao_de_Estacionamento.Infraestrutura.Orm.Compartilhado;
using Gestao_de_Estacionamento.Infraestrutura.Orm.ModuloVeiculo;

namespace Gestao_de_Estacionamento.Testes.Integracao.Compartilhado;

[TestClass]
public class TestFixture
{
    protected AppDbContext dbContext;

    protected RepositorioVeiculoEmOrm repositorioVeiculo;
}
