namespace Gestao_de_Estacionamento_web_api.Models.ModuloVaga;

public record SelecionarVagaPorIdRequest (Guid id);

public record SelecionarVagaPorIdResponse(
    Guid Id,
    int Numero,
    char Zona,
    bool Status
    );
