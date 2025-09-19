namespace Domain.Models
{
    public class Usuario : EntityBase
    {
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string Senha { get; set; }
        public required DateTime DataNascimento { get; set; }
        public string Endereco { get; set; }
        public required byte TipoUsuario { get; set; }

        #region [Navegacao]
        public virtual ICollection<Pedido> Pedidos { get; set; }
        #endregion
    }
}
 