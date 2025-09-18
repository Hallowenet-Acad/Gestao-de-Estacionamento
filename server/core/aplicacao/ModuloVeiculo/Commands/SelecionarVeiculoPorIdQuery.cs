using FluentResults;
using MediatR;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

public record SelecionarVeiculoPorIdQuery (Guid Id) : IRequest<Result<SelecionarVeiculoPorIdResult>>;

public record SelecionarVeiculoPorIdResult (
    Guid Id,
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    );

