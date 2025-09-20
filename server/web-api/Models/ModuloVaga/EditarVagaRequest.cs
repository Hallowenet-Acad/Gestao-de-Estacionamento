namespace Gestao_de_Estacionamento_web_api.Models.ModuloVaga;

public record EditarVagaRequest(
    int Numero,
    char Zona,
    bool Status
    );

public record EditarVagaResponse(
    int Numero,
    char Zona,
    bool Status
    );
