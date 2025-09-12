using FluentResults;
using Gestao_de_Estacionamento.Core.Aplicacao.ModuloAutenticacao.Commands;
using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Gestao_de_Estacionamento.Core.Aplicacao.ModuloAutenticacao.Handlers;

public class SairCommandHandler(
    SignInManager<Usuario> signInManager
) : IRequestHandler<SairCommand, Result>
{
    public async Task<Result> Handle(SairCommand request, CancellationToken cancellationToken)
    {
        await signInManager.SignOutAsync();

        return Result.Ok();
    }
}
