using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Configurations
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0).HasColumnType("int").UseIdentityColumn();
            builder.Property(x => x.DataCriacao).HasColumnOrder(1).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.UsuarioId).HasColumnOrder(2).HasColumnType("int").IsRequired();
            builder.Property(x => x.GameId).HasColumnOrder(3).HasColumnType("int").IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany(y => y.Pedidos)
                .HasPrincipalKey(x => x.Id);

            builder.HasOne(x => x.Game)
                .WithMany(y => y.Pedidos)
                .HasPrincipalKey(x => x.Id);
        }
    }
} 
