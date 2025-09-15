using Gestao_de_Estacionamento.Core.Dominio.Compartilhado;
using System.Diagnostics.CodeAnalysis;

namespace Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;

public class Veiculo : EntidadeBase<Veiculo>
{
    public string Placa { get; set; }
    public string Modelo { get; set; }
    public string Cor { get; set; }
    public string CPFHospede { get; set; }

    [ExcludeFromCodeCoverage]
    public Veiculo() { }

    public Veiculo(string placa) : this()
    {
        Id = Guid.NewGuid();
        Placa = placa;
    }
    public Veiculo(string placa, string modelo, string cor, string cpfHospede)
    {
        Placa = placa;
        Modelo = modelo;
        Cor = cor;
        CPFHospede = cpfHospede;
    }
    public override void AtualizarRegistro(Veiculo registroAtualizado)
    {
        Placa = registroAtualizado.Placa;
        Modelo = registroAtualizado.Modelo;
        Cor = registroAtualizado.Cor;
        CPFHospede = registroAtualizado.CPFHospede;
    }
    public override bool Equals(object? obj)
    {
        return obj is Veiculo veiculo &&
               Id == veiculo.Id &&
               Placa == veiculo.Placa &&
               Modelo == veiculo.Modelo &&
               Cor == veiculo.Cor &&
               CPFHospede == veiculo.CPFHospede;
    }
}
