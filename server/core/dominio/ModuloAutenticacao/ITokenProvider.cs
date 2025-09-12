

namespace Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;

public interface ITokenProvider
{
    AccessToken GerarAccessToken(Usuario usuario);
}

public record AccessToken(
    string Chave,
    DateTime Expiracao,
    UsuarioAutenticado UsuarioAutenticado
);
