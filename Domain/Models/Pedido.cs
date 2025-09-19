namespace Domain.Models
{
    public class Pedido : EntityBase
    {
        public required int UsuarioId { get; set; }
        public required int GameId { get; set; }

        #region [Navegacao]
        public virtual Usuario Usuario { get; set; }
        public virtual Game Game { get; set; }
        #endregion
    }
}
 