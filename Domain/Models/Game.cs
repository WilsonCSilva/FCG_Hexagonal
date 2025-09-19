namespace Domain.Models
{
    public class Game : EntityBase
    {
        public required string Produtora { get; set; }
        public required string Descricao { get; set; }
        public required decimal Preco { get; set; }
        public required DateTime DataLancamento { get; set; }

        #region [Navegacao]
        public virtual ICollection<Pedido> Pedidos { get; set; }
        #endregion
    }
}
 