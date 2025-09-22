using Gestao_de_Estacionamento.Core.Dominio.ModuloEstacionamento;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.ModuloEstacionamento;

public class MapeadorEstacionamento : IEntityTypeConfiguration<Estacionamento>
{
    public void Configure(EntityTypeBuilder<Estacionamento> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(e => e.UsuarioId)
            .IsRequired();

        builder.Property(e => e.Nome)
            .IsRequired();

        builder.HasMany(e => e.Vagas)
            .WithOne(v => v.Estacionamento)
            .HasForeignKey(v => v.EstacionamentoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => new { e.UsuarioId, e.Nome })
            .IsUnique();
    }
}
