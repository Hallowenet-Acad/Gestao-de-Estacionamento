using AutoMapper;
using FluentResults;
using Gestao_de_Estacionamento.Core.Aplicacao.Compartilhado;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class SelecionarVagaQueryHandler (
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<SelecionarVagaQueryHandler> logger
    ) : IRequestHandler<SelecionarVagasQuery, Result<SelecionarVagasResult>>
{
    public async Task<Result<SelecionarVagasResult>> Handle(SelecionarVagasQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cacheQuery = query.Quantidade.HasValue ? $"q={query.Quantidade.Value}" : "q=all";
            var cacheKey = $"veiculos:u={tenantProvider.UsuarioId.GetValueOrDefault()}:{cacheQuery}";

            var jsonString = await cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrWhiteSpace(jsonString))
            {
                var registrosEmCache = JsonSerializer.Deserialize<SelecionarVagasResult>(jsonString);

                if (registrosEmCache is not null)
                    return Result.Ok(registrosEmCache);
            }

            var registros = query.Quantidade.HasValue ?
                await repositorioVaga.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioVaga.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarVagasResult>(registros);

            var jsonPayLoad = JsonSerializer.Serialize(result);

            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60) };

            await cache.SetStringAsync(cacheKey, jsonPayLoad, cacheOptions, cancellationToken);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro ao Selecionar {@Registro}.",
                query
                );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
