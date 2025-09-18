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

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Handlers;

public record CadastrarVeiculoCommandHandler(
    IRepositorioVeiculo repositorioVeiculo,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<CadastrarVeiculoCommand> validator,
    ILogger<CadastrarVeiculoCommandHandler> logger
    ) : IRequestHandler<CadastrarVeiculoCommand, Result<CadastrarVeiculoResult>>
{
    public async Task<Result<CadastrarVeiculoResult>> Handle(
        CadastrarVeiculoCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if(!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

            return Result.Fail(erroFormatado);
        }

        var registros = await repositorioVeiculo.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Placa.Equals(command.Placa)))
            return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um veículo registrado com esta placa."));

        try
        {
            var veiculo = mapper.Map<Veiculo>(command);

            veiculo.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

            await repositorioVeiculo.CadastrarRegistroAsync(veiculo);

            await unitOfWork.CommitAsync();

            var cacheKey = $"veiculos:u={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = mapper.Map<CadastrarVeiculoResult>(veiculo);

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
