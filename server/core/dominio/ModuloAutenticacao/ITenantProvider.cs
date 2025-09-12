namespace Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;

public interface ITenantProvider
{
    Guid? UsuarioId { get; }
    bool IsInRole(string role);
}
