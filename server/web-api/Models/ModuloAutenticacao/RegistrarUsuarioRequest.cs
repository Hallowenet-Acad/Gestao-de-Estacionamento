namespace Gestao_de_Estacionamento_web_api.Models.ModuloAutenticacao;

public record class RegistrarUsuarioRequest(
string NomeUsuario,
string Senha,
string ConfirmarSenha
);