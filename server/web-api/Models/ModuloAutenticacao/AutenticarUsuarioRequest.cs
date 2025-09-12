namespace Gestao_de_Estacionamento_web_api.Models.ModuloAutenticacao;

public record class AutenticarUsuarioRequest(
string Email,
string Senha
);
