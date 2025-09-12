using FluentResults;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;

public record RegistrarUsuarioCommand(string NomeCompleto, string Email, string Senha, string ConfirmarSenha)
    : IRequest<Result<AccessToken>>;
