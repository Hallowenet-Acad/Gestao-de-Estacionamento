using Gestao_de_Estacionamento.Core.Aplicacao;
using Gestao_de_Estacionamento_web_api.AutoMapper;
using Gestao_de_Estacionamento_web_api.Orm;
using Gestao_de_Estacionamento_web_api.Swagger;
using Gestao_de_Estacionamento.Infraestrutura.Orm;
using System.Text.Json.Serialization;
using Gestao_de_Estacionamento_web_api.Identity;

namespace Gestao_de_Estacionamento_web_api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services
            .AddCamadaAplicacao(builder.Logging, builder.Configuration)
            .AddCamadaInfraestruturaOrm(builder.Configuration);

        builder.Services.AddAutoMapperProfiles(builder.Configuration);

        builder.Services.AddIdentityProviders();
        builder.Services.AddJwtAuthentication(builder.Configuration);

        builder.Services
            .AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddSwaggerConfig();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.ApplyMigrations();

            app.UseSwagger();
            app.UseSwaggerUI(options =>  // Define a UI do Swagger como a rota padrão da aplicação.
            {                                      // Tip pega em > https://aka.ms/aspnetcore/swashbuckle
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
