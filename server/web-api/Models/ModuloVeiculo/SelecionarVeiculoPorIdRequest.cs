namespace Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo;

public record SelecionarVeiculoPorIdRequest (Guid Id);

public record SelecionarVeiculoPorIdResponse(
    Guid Id,
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    );