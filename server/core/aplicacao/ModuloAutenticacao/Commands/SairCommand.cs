using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;

public record SairCommand : IRequest<Result>;
