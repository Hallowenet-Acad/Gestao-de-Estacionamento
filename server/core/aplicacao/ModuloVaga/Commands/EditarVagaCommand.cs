using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record EditarVagaCommand(
    Guid Id,
    int Numero,
    char Zona,
    bool Status
    ) : IRequest<Result<EditarVagaResult>>;

public record EditarVagaResult(
    int Numero,
    char Zona,
    bool Status
    );
