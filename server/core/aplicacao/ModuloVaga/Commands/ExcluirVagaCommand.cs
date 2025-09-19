using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record ExcluirVagaCommand(Guid Id) : IRequest<Result<ExcluirVagaResult>>;

public record ExcluirVagaResult();

