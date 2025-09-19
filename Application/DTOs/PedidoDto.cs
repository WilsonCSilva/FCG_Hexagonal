namespace Core.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string DataCriacao { get; set; }
        public required int UsuarioId { get; set; }
        public required int GameId { get; set; }

        #region [Navegacao]
        public UsuarioDto Usuario { get; set; }
        public GameDto Game { get; set; }
        #endregion 
    }
}
