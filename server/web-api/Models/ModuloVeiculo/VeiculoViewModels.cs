using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using System.ComponentModel.DataAnnotations;

namespace Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo;

public abstract class VeiculoViewModels
{
    [Required(ErrorMessage = "O campo \"Placa\" é obrigatório.")]
    [MinLength(7, ErrorMessage = "O campo \"Placa\" deve ter no mínimo 7 caracteres.")]
    [MaxLength(7, ErrorMessage = "O campo \"Placa\" deve ter no máximo 7 caracteres.")]
    public string Placa { get; set; }

    [Required(ErrorMessage = "O campo \"Modelo\" é obrigatório.")]
    public string Modelo { get; set; }

    [Required(ErrorMessage = "O campo \"Cor\" é obrigatório.")]
    public string Cor { get; set; }

    [Required(ErrorMessage = "O campo \"CPF do Hóspede\" é obrigatório.")]
    [MinLength(11, ErrorMessage = "O campo \"CPF do Hóspede\" deve ter no mínimo 11 caracteres.")]
    [MaxLength(11, ErrorMessage = "O campo \"CPF do Hóspede\" deve ter no máximo 11 caracteres.")]
    public string CPFHospede { get; set; }

    public static Veiculo ParaEntidade(VeiculoViewModels viewModel)
    {
        return new Veiculo(viewModel.Placa, viewModel.Modelo, viewModel.Cor, viewModel.CPFHospede);
    }

    public class CadastrarVeiculoViewModel : VeiculoViewModels
    {
        public CadastrarVeiculoViewModel() { }
    }

    public class EditarVeiculoViewModel : VeiculoViewModels
    {
        public Guid Id { get; set; }
        public EditarVeiculoViewModel() { }

        public EditarVeiculoViewModel(Guid id, string placa, string modelo, string cor, string cpfHospede) : this()
        {
            Id = id;
            Placa = placa;
            Modelo = modelo;
            Cor = cor;
            CPFHospede = cpfHospede;
        }
    }   

    public class ExcluirVeiculoViewModel : VeiculoViewModels
    {
        public Guid Id { get; set; }
        public ExcluirVeiculoViewModel() { }
        public ExcluirVeiculoViewModel(Guid id, string placa, string modelo, string cor, string cpfHospede) : this()
        {
            Id = id;
            Placa = placa;
            Modelo = modelo;
            Cor = cor;
            CPFHospede = cpfHospede;
        }
    }

    public class VisualizarVeiculosViewModel
    {
        public List<DetalhesVeiculoViewModel> Registros { get; set; }

        public VisualizarVeiculosViewModel(List<Veiculo> Veiculos)
        {
            Registros = Veiculos
                .Select(DetalhesVeiculoViewModel.ParaDetalhesVM)
                .ToList();
        }
    }

    public class DetalhesVeiculoViewModel
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Cor { get; set; }
        public string CPFHospede { get; set; }
        public DetalhesVeiculoViewModel(Guid id, string placa, string modelo, string cor, string cpfHospede)
        {
            Id = id;
            Placa = placa;
            Modelo = modelo;
            Cor = cor;
            CPFHospede = cpfHospede;
        }

        public static DetalhesVeiculoViewModel ParaDetalhesVM(Veiculo veiculo)
        {
            return new DetalhesVeiculoViewModel(
                veiculo.Id,
                veiculo.Placa,
                veiculo.Modelo,
                veiculo.Cor,
                veiculo.CPFHospede
                );
        }
    }
}
