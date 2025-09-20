namespace Gestao_de_Estacionamento_web_api.Models.ModuloVaga;

public record SelecionarVagasRequest (int? Quantidade);

public record SelecionarVagasResponse(
    int Quantidade
    );
