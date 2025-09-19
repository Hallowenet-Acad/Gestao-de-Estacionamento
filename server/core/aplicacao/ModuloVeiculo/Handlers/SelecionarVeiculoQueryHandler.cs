using AutoMapper;
using FluentResults;
using FluentValidation;
using Gestao_de_Estacionamento.Core.Aplicacao.Compartilhado;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Handlers;

internal class SelecionarVeiculoQueryHandler (
    IRepositorioVeiculo repositorioVeiculo,
    ITenantProvider tenantProvider,
    IMapper mapper,
    IDistributedCache cache,
    ILogger<SelecionarVeiculoQueryHandler> logger
    ) : IRequestHandler<SelecionarVeiculosQuery, Result<SelecionarVeiculosResult>>
{
    public async Task<Result<SelecionarVeiculosResult>> Handle(SelecionarVeiculosQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var cacheQuery = query.Quantidade.HasValue ? $"q={query.Quantidade.Value}" : "q=all";
            var cacheKey = $"veiculos:u={tenantProvider.UsuarioId.GetValueOrDefault()}:{cacheQuery}";

            var jsonString = await cache.GetStringAsync(cacheKey, cancellationToken);

            if(!string.IsNullOrWhiteSpace(jsonString))
            {
                var registrosEmCache = JsonSerializer.Deserialize<SelecionarVeiculosResult>(jsonString);

                if (registrosEmCache is not null)
                    return Result.Ok(registrosEmCache);
            }

            var registros = query.Quantidade.HasValue ?
                await repositorioVeiculo.SelecionarRegistrosAsync(query.Quantidade.Value) :
                await repositorioVeiculo.SelecionarRegistrosAsync();

            var result = mapper.Map<SelecionarVeiculosResult>(registros);

            var jsonPayload = JsonSerializer.Serialize(result);

            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60) };

            await cache.SetStringAsync(cacheKey, jsonPayload, cacheOptions, cancellationToken);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ocorreu um erro ao selecionar {@Registros}.",
                query
                );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
