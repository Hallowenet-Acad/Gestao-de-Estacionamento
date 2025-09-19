using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record SelecionarVagaPorIdQuery(Guid Id) : IRequest<Result<SelecionarVagaPorIdResult>>;

public record SelecionarVagaPorIdResult(
    Guid Id,
    int Numero,
    char Zona,
    bool Status
    );
