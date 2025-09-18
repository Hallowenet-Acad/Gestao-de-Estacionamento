using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record ExcluirVeiculoCommand(
    Guid Id,
    string Placa, 
    string Modelo,
    string Cor,
    string CPFHospede
    ) : IRequest<Result<ExcluirVeiculoResult>>;

public record ExcluirVeiculoResult(
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    );

