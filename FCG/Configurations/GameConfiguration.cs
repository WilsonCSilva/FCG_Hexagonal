using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(0).HasColumnType("int").UseIdentityColumn();
            builder.Property(x => x.DataCriacao).HasColumnOrder(1).HasColumnType("date").IsRequired();
            builder.Property(x => x.Nome).HasColumnOrder(2).HasColumnType("varchar(200)").IsRequired();
            builder.Property(x => x.Produtora).HasColumnOrder(3).HasColumnType("varchar(100)").IsRequired();
            builder.Property(x => x.Descricao).HasColumnOrder(4).HasColumnType("varchar(1000)").IsRequired();
            builder.Property(x => x.Preco).HasColumnOrder(5).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.DataLancamento).HasColumnOrder(6).HasColumnType("date").IsRequired();
        }
    }
} 
