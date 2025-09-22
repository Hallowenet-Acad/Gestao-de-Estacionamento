using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using System.Diagnostics.CodeAnalysis;

namespace Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento.ModuloVaga;

public class Vaga : EntidadeBase<Vaga>
{
    public int Numero { get; set; }
    public ZonaEstacionamento Zona { get; set; }
    public Guid? VeiculoId { get; set; }
    public Veiculo? Veiculo { get; set; }
    public StatusVaga Status => Veiculo is null ? StatusVaga.Livre : StatusVaga.Ocupada;
    public Guid EstacionamentoId { get; set; }
    public Estacionamento Estacionamento { get; set; }
    public bool EstaOcupada => Veiculo is not null;

    public void Ocupar(Veiculo veiculo)
    {
        if (veiculo is not null)
            return;

        Veiculo = veiculo;
        VeiculoId = veiculo.Id;
    }

    public void Liberar()
    {
        if (Veiculo is null)
            return;

        Veiculo = null;
        VeiculoId = null;
    }

    public Vaga() { }

    public Vaga(int numero) : this()
    {
        Id = Guid.NewGuid();
        Numero = numero;
    }

    public Vaga(int numero, ZonaEstacionamento zona) : this(numero)
    {
        Numero = numero;
        Zona = zona;
    }

    public override void AtualizarRegistro(Vaga registroAtualizado)
    {
        Numero = registroAtualizado.Numero;
        Zona = registroAtualizado.Zona;
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
