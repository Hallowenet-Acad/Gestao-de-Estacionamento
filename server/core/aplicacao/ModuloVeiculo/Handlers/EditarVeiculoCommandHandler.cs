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

public class EditarVeiculoCommandHandler (
    IRepositorioVeiculo repositorioVeiculo,
    ITenantProvider tenantProvider,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDistributedCache cache,
    IValidator<EditarVeiculoCommand> validator, 
    ILogger<EditarVeiculoCommandHandler> logger
    ) : IRequestHandler<EditarVeiculoCommand, Result<EditarVeiculoResult>>
{
    public async Task<Result<EditarVeiculoResult>> Handle(EditarVeiculoCommand command, CancellationToken cancellationToken)
    {
        var resultadoValidacao = await validator.ValidateAsync(command, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors.Select(e => e.ErrorMessage);

            var erroFormatado = ResultadosErro.RequisicaoInvalidaErro(erros);

            return Result.Fail(erroFormatado);

        }

        var registros = await repositorioVeiculo.SelecionarRegistrosAsync();

        if (registros.Any(i => i.Id.Equals(command.Id) && i.Placa.Equals(command.Placa)))
           return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um veículo registrado com esta placa."));

        try
        {
            var veiculoEditado = mapper.Map<Veiculo>(command);

            await repositorioVeiculo.EditarRegistroAsync(command.Id, veiculoEditado);

            await unitOfWork.CommitAsync();

            var cacheKey = $"veiculos:i={tenantProvider.UsuarioId.GetValueOrDefault()}:q=all";

            await cache.RemoveAsync(cacheKey, cancellationToken);

            var result = mapper.Map<EditarVeiculoResult>(veiculoEditado);

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();

            logger.LogError(
                ex,
                "Ocorreu um erro durante a edição do {@Registro}",
                command
                );

            return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
        }
    }
}
