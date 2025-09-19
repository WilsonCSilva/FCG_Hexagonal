using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0).HasColumnType("int").UseIdentityColumn();
            builder.Property(x => x.DataCriacao).HasColumnOrder(1).HasColumnType("date").IsRequired();
            builder.Property(x => x.Nome).HasColumnOrder(2).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Email).HasColumnOrder(3).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Senha).HasColumnOrder(4).HasColumnType("varchar(50)");
            builder.Property(x => x.Endereco).HasColumnOrder(5).HasColumnType("varchar(200)");
            builder.Property(x => x.DataNascimento).HasColumnOrder(6).HasColumnType("date").IsRequired();
            builder.Property(x => x.TipoUsuario).HasColumnOrder(7).HasColumnType("tinyint").IsRequired();
        }
    }
} 
