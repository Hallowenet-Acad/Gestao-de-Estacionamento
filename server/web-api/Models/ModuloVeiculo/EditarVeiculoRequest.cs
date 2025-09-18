namespace Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo;

public record EditarVeiculoRequest (
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    );

public record EditarVeiculoResponse(
    string Placa,
    string Modelo,
    string Cor,
    string CPFHospede
    );
