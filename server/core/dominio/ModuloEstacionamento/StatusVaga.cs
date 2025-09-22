using System.ComponentModel.DataAnnotations;

namespace Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento;

public enum StatusVaga
{
    [Display(Name = "Livre")]
    Livre,
    [Display(Name = "Ocupada")]
    Ocupada
}
