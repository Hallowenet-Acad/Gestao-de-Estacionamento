using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record ExcluirVeiculoCommand(Guid Id) : IRequest<Result<ExcluirVeiculoResult>>;

public record ExcluirVeiculoResult();

