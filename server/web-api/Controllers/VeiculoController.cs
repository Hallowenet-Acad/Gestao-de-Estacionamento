using AutoMapper;
using Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Estacionamento_web_api.Controllers;


[ApiController]
[Authorize]
[Route("veiculos")]
public class VeiculoController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarVeiculoResponse>> Cadastrar(CadastrarVeiculoRequest request)
    {
        var command = mapper.Map(CadastrarVeiculoCommand)
    }
}
