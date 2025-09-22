using AutoMapper;
using FluentResults;
using FluentValidation;
using Gestao_de_Estacionamento.Core.Aplicacao.Compartilhado;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Commands;
using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento.ModuloVaga;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVaga.Handlers;

public class CadastrarVagaCommandHandler(
    IRepositorioVaga repositorioVaga,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<CadastrarVagaCommand> validator,
    ILogger<CadastrarVagaCommandHandler> logger
    ) : IRequestHandler<CadastrarVagaCommand, Result<CadastrarVagaResult>>
{
    public async Task<Result<CadastrarVagaResult>> Handle(
        CadastrarVagaCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if(!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);
            
            return Result.Fail(erroFormatado);
        }

        var registros = await repositorioVaga.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Numero.Equals(command.numero)) && registros.Any(i => i.Zona.Equals(command.zona)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe uma vaga nessa zona com esse número."));
        
        try
        {
            var vaga = mapper.Map<Vaga>(command);

            vaga.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

            await repositorioVaga.CadastrarRegistroAsync(vaga);

            await unitOfWork.CommitAsync();

            var cacheKey = $"vagas:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = mapper.Map<CadastrarVagaResult>(vaga);

            return Result.Ok(result);
        }

        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante o registro do {@Registro}.",
                command
                );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
