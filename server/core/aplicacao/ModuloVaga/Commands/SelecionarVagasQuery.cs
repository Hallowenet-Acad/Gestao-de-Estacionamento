using FluentResults;
using MediatR;
using System.Collections.Immutable;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;

public record SelecionarVagasQuery(int? Quantidade) : IRequest<Result<SelecionarVagasResult>>;

public record SelecionarVagasResult(ImmutableList<SelecionarVagasDto> Vagas);

public record SelecionarVagasDto(
    Guid Id, int Numero, char Zona, bool Status);
