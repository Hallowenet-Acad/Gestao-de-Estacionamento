using AutoMapper;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloVeiculo.Commands;
using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using System.Collections.Immutable;

namespace Gestao_de_Estacionamento.Core.Aplicacao.AutoMapper;

public class VeiculoMappingProfile : Profile
{
    public VeiculoMappingProfile()
    {
        CreateMap<CadastrarVeiculoCommand, Veiculo>();
        CreateMap<Veiculo, CadastrarVeiculoResult>();

        CreateMap<EditarVeiculoCommand, Veiculo>();
        CreateMap<Veiculo, EditarVeiculoResult>();

        CreateMap<Veiculo, SelecionarVeiculoPorIdResult>()
            .ConvertUsing(src => new SelecionarVeiculoPorIdResult(
                src.Id,
                src.Placa,
                src.Modelo,
                src.Cor,
                src.CPFHospede
                ));

        CreateMap<Veiculo, SelecionarVeiculosDto>();

        CreateMap<IEnumerable<Veiculo>, SelecionarVeiculosResult>()
            .ConvertUsing((src, dest, ctx) =>
            new SelecionarVeiculosResult(
                src?.Select(c => ctx.Mapper.Map<SelecionarVeiculosDto>(c)).ToImmutableList() ?? ImmutableList<SelecionarVeiculosDto>.Empty
                )
            );
    }
}
