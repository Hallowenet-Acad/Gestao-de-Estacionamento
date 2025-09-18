using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record EditarVeiculoCommand (
    Guid Id,
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    ) : IRequest<Result<EditarVeiculoResult>>;

public record EditarVeiculoResult(
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    );


