using Gestao_de_Estacionamento.Core.Dominio.ModuloVeiculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gestao_de_Estacionamento.Infraestrutura.Orm.ModuloVeiculo;

public class MapeadorVeiculoEmOrm : IEntityTypeConfiguration<Veiculo>
{
    public void Configure(EntityTypeBuilder<Veiculo> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Placa)
            .HasMaxLength(7)
            .IsRequired();

        builder.Property(x => x.Modelo)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Cor)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.CPFHospede)
            .HasMaxLength(11)
            .IsRequired();
    }
}
