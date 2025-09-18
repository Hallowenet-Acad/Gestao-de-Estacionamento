using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record CadastrarVeiculoCommand(
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    ) : IRequest<Result<CadastrarVeiculoResult>>;

public record CadastrarVeiculoResult(Guid Id);
