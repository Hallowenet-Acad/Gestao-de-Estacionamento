using Gestao_de_Estacionamento.Core.Dominio.ModuloAutenticacao;
using System.Security.Claims;

namespace Gestao_de_Estacionamento_web_api.Identity
{
    public class IdentityTenantProvider(IHttpContextAccessor contextAccessor) : ITenantProvider
    {
        public Guid? UsuarioId
        {
            get
            {
                Claim? claimId = contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);

                if (claimId is null)
                    return null;

                return Guid.Parse(claimId.Value);
            }
        }

        public bool IsInRole(string role)
        {
            return contextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }
    }
}
