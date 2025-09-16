using FluentResults;
using Gestao_de_Estacionamento.Core.Aplicacao.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao_de_Estacionamento.Aplicacao.ModuloVeiculo
{
    public class VeiculoAppService
    {
        private readonly ITenantProvider tenantProvider;
        private readonly IRepositorio<Veiculo> repositorioVeiculo;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<VeiculoAppService> logger;

        public VeiculoAppService(
            ITenantProvider tenantProvider,
            IRepositorio<Veiculo> repositorioVeiculo,
            IUnitOfWork unitOfWork,
            ILogger<VeiculoAppService> logger)
        {
            this.tenantProvider = tenantProvider;
            this.repositorioVeiculo = repositorioVeiculo;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<Result> Cadastrar(Veiculo veiculo)
        {
            var registros = await repositorioVeiculo.SelecionarRegistrosAsync();

            if (registros.Exists(v => v.Placa.Equals(veiculo.Placa, StringComparison.OrdinalIgnoreCase)))
            {
                return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um veículo registrado com esta placa."));
            }

            try
            {
                // Se você precisar associar o TenantId ao veículo, descomente a linha abaixo.
                veiculo.UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault();

                await repositorioVeiculo.CadastrarRegistroAsync(veiculo);
                await unitOfWork.CommitAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Ocorreu um erro durante o registro do {@Registro}.", veiculo);
                return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
            }
        }

        public async Task<Result> Editar(Guid id, Veiculo veiculoEditado)
        {
            var registros = await repositorioVeiculo.SelecionarRegistrosAsync();

            if (registros.Exists(v => v.Id != id && v.Placa.Equals(veiculoEditado.Placa, StringComparison.OrdinalIgnoreCase)))
            {
                return Result.Fail(ResultadosErro.RegistroDuplicadoErro("Já existe um veículo registrado com esta placa."));
            }

            try
            {
                await repositorioVeiculo.EditarRegistroAsync(id, veiculoEditado);
                await unitOfWork.CommitAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Ocorreu um erro durante a edição do registro {@Registro}.", veiculoEditado);
                return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
            }
        }

        public async Task<Result> Excluir(Guid id)
        {
            try
            {
                bool exclusaoConcluida = await repositorioVeiculo.ExcluirRegistroAsync(id);

                if (exclusaoConcluida == false)
                {
                    return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(id));
                }

                await unitOfWork.CommitAsync();
                return Result.Ok();
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                logger.LogError(ex, "Ocorreu um erro durante a exclusão do registro {Id}.", id);
                return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
            }
        }

        public async Task<Result<Veiculo>> SelecionarPorId(Guid id)
        {
            try
            {
                var registroSelecionado = await repositorioVeiculo.SelecionarRegistroPorIdAsync(id);

                if (registroSelecionado == null)
                {
                    return Result.Fail(ResultadosErro.RegistroNaoEncontradoErro(id));
                }

                return Result.Ok(registroSelecionado);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro durante a seleção do registro {Id}.", id);
                return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
            }
        }

        public async Task<Result<List<Veiculo>>> SelecionarTodos()
        {
            try
            {
                var registros = await repositorioVeiculo.SelecionarRegistrosAsync();
                return Result.Ok(registros);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro durante a seleção de registros.");
                return Result.Fail(ResultadosErro.ExcecaoInternaErro(ex));
            }
        }
    }
}
