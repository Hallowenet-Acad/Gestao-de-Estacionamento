namespace Gestao_de_Estacionamento_web_api.Models.ModuloVaga;

public record CadastrarVagaRequest(
    int numero,
    char zona,
    bool status
    );

public record CadastrarVagaResponse(Guid Id);
