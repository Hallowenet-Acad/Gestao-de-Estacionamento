using AutoMapper;
using FluentResults;
using Gestao_de_Estacionamento.Core.Aplicacao.Compartilhado;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;
using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class SelecionarVagaPorIdQueryHandler (
    IMapper mapper,
    IRepositorioVaga repositorioVaga,
    ILogger<SelecionarVagaPorIdQueryHandler> logger
    ) : IRequestHandler<SelecionarVagaPorIdQuery, Result<SelecionarVagaPorIdResult>>
{
    public async Task<Result<SelecionarVagaPorIdResult>> Handle(SelecionarVagaPorIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var registro = await repositorioVaga.SelecionarRegistroPorIdAsync(query.Id);

            if (registro is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(query.Id));

            var result = mapper.Map<SelecionarVagaPorIdResult>(registro);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro ao selecionar o registro {@Registro}.",
                query
                );
            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
