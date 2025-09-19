using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record CadastrarVagaCommand(
    int numero,
    char zona,
    bool status
    ) : IRequest<Result<CadastrarVagaResult>>;

public record CadastrarVagaResult(Guid Id);

