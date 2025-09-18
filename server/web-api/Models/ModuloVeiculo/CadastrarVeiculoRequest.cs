namespace Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo;

public record CadastrarVeiculoRequest (
    string Placa, 
    string Modelo, 
    string Cor, 
    string CPFHospede
    );

public record CadastrarVeiculoResponse(Guid Id);