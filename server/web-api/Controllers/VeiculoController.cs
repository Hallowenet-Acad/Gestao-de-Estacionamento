using AutoMapper;
using FluentResults;
using Gestao_de_Estacionamento.Aplicacao.ModuloVeiculo;
using Gestao_de_Estacionamento_web_api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Gestao_de_Estacionamento_web_api.Models.ModuloVeiculo.VeiculoViewModels;

namespace Gestao_de_Estacionamento_web_api.Controllers;


[ApiController]
[Authorize]
[Route("veiculos")]
public class VeiculoController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarVeiculoViewModel>>
}
