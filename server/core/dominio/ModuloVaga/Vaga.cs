using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using System.Diagnostics.CodeAnalysis;

namespace Gestao_de_Estacionamento.Core.Dominio.ModuloVaga;

public class Vaga : EntidadeBase<Vaga>
{
    public int Numero { get; set; }
    public char Zona { get; set; }
    public bool Status { get; set; } // true = ocupada, false = livre


    [ExcludeFromCodeCoverage]
    public Vaga() { }

    public Vaga(int numero) : this()
    {
        Id = Guid.NewGuid();
        Numero = numero;
    }

    public Vaga(int numero, char zona, bool status) : this(numero)
    {
        Numero = numero;
        Zona = zona;
        Status = status;
    }

    public override void AtualizarRegistro(Vaga registroAtualizado)
    {
        Numero = registroAtualizado.Numero;
        Zona = registroAtualizado.Zona;
        Status = registroAtualizado.Status;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vaga vaga &&
               Id == vaga.Id &&
               Numero == vaga.Numero &&
               Zona == vaga.Zona &&
               Status == vaga.Status;
    }
}
