using AutoMapper;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;
using Gestao_de_Estacionamento_web_api.Models.ModuloAutenticacao;

namespace Gestao_de_Estacionamento_web_api.AutoMapper;

public class AutenticacaoModelsMappingProfile : Profile
{
    public AutenticacaoModelsMappingProfile()
    {
        CreateMap<RegistrarUsuarioRequest, RegistrarUsuarioCommand>();
        CreateMap<AutenticarUsuarioRequest, AutenticarUsuarioCommand>();
    }
}
