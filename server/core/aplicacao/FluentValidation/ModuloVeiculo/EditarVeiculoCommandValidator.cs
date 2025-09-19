using FluentValidation;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;

namespace Gestao_de_Estacionamento.Core.Aplicacao.FluentValidation.ModuloVeiculo;

public class EditarVeiculoCommandValidator : AbstractValidator<EditarVeiculoCommand>
{
    public EditarVeiculoCommandValidator()
    {
        RuleFor(x => x.Placa)
           .NotEmpty().WithMessage("A placa do veículo é obrigatória.")
           .MinimumLength(7).WithMessage("A placa do veículo deve ter no mínimo 7 caracteres.")
           .MaximumLength(7).WithMessage("A placa do veículo deve ter no máximo 7 caracteres.");
        RuleFor(x => x.Modelo)
            .NotEmpty().WithMessage("O modelo do veículo é obrigatório.")
            .MinimumLength(2).WithMessage("O modelo do veículo deve ter no mínimo 2 caracteres.")
            .MaximumLength(50).WithMessage("O modelo do veículo deve ter no máximo 50 caracteres.");
        RuleFor(x => x.Cor)
            .NotEmpty().WithMessage("A cor do veículo é obrigatória.")
            .MinimumLength(2).WithMessage("A cor do veículo deve ter no mínimo 2 caracteres.")
            .MaximumLength(20).WithMessage("A cor do veículo deve ter no máximo 20 caracteres.");
        RuleFor(x => x.CPFHospede)
            .NotEmpty().WithMessage("O CPF do hóspede é obrigatório.")
            .Matches(@"^\d{11}$").WithMessage("O CPF do hóspede deve conter exatamente 11 dígitos numéricos.");
    }
}
