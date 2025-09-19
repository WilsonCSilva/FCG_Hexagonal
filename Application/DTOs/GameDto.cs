namespace Core.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public string DataCriacao { get; set; }
        public required string Nome { get; set; }
        public required string Produtora { get; set; }
        public required string Descricao { get; set; }
        public required string Preco { get; set; }
        public required string DataLancamento { get; set; }

        public virtual string Email { get; set; }

        #region [Navegacao]
        public ICollection<PedidoDto> Pedidos { get; set; }
        #endregion 
    }
}
