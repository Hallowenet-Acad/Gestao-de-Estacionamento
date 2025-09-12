using FluentResults;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;

public record AutenticarUsuarioCommand(string Email, string Senha) : IRequest<Result<AccessToken>>;
