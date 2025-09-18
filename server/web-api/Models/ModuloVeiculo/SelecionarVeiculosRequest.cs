namespace Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo;

public record SelecionarVeiculosRequest(int? Quantidade);

public record SelecionarVeiculosResponse(
    int Quantidade
    );
