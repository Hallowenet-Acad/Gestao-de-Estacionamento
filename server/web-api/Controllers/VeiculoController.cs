using FluentResults;
using Gestao_de_Estacionamento.Aplicacao.ModuloVeiculo;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_de_Estacionamento_web_api.Controllers;

[Route("veiculos")]
public class VeiculoController : Controller
{
    private readonly VeiculoAppService veiculoAppService;
    
    public VeiculoController(VeiculoAppService veiculoAppService)
    {
        this.veiculoAppService = veiculoAppService;
    }

    //[HttpGet]
    //public IActionResult Index()
    //{
    //    var resultado = veiculoAppService.SelecionarTodos();

    //    if (resultado.IsFailed)
    //        return this.RedirecionarParaNotificacaoHome(resultado.ToResult());

    //    var visualizarVM = new VisualizarVeiculosViewModel(resultado.Value);

    //    this.ObterNotificacaoPendente();

    //    return View(visualizarVM);  
    //}
}
