using FluentResults;
using Gestao_de_Estacionamento.Core.Aplicacao.Compartilhado;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;
using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using Gestao_de_Estacionamento.Core.Dominio.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class ExcluirVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IDistributedCache cache,
    ILogger<ExcluirVagaCommandHandler> logger
    ) : IRequestHandler<ExcluirVagaCommand, Result<ExcluirVagaResult>>
{
    public async Task<Result<ExcluirVagaResult>> Handle(ExcluirVagaCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var vagaSelecionada = await repositorioVaga.SelecionarRegistroPorIdAsync(command.Id);

            if (vagaSelecionada is null)
                return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(command.Id));

            await repositorioVaga.ExcluirRegistroAsync(command.Id);

            await unitOfWork.CommitAsync();

            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = new ExcluirVagaResult();

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro na exclusão de {@Registro}",
                command
                );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
