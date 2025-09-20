using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Estacionamento_web_api.Controllers;

[ApiController]
[Authorize]
[Route("vagas")]
public class VagaController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<CadastrarVagaRe>>
}
