namespace Gestao_de_Estacionamento.Core.Dominio.Compartilhado;

public interface IUnitOfWork
{
    public Task CommitAsync();
    public Task RollbackAsync();
}
