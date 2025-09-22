using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento.ModuloVaga;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.ModuloEstacionamento;

public class MapeadorVaga : IEntityTypeConfiguration<Vaga>
{
    public void Configure(EntityTypeBuilder<Vaga> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(v => v.UsuarioId)
            .IsRequired();

        builder.Property(v => v.Numero)
            .IsRequired();

        builder.Property(v => v.Zona)
            .IsRequired();

        builder.HasOne(v => v.Veiculo)
            .WithOne(v => v.Vaga)
            .HasForeignKey<Vaga>(v => v.VeiculoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Ignore(v => v.Status);

        builder.HasOne(v => v.Estacionamento)
            .WithMany(e => e.Vagas)
            .HasForeignKey(v => v.EstacionamentoId)
            .IsRequired();

        builder.HasIndex(v => new { v.UsuarioId, v.VeiculoId })
            .IsUnique();

        builder.HasIndex(v => new { v.EstacionamentoId, v.Zona, v.Numero })
            .IsUnique();
    }
}
