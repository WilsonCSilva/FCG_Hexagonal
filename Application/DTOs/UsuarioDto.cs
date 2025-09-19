namespace Core.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string DataCriacao { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public string Senha { get; set; }
        public string Endereco { get; set; }
        public required string DataNascimento { get; set; }
        public required Int16 TipoUsuario { get; set; }

        #region [Navegacao]
        public ICollection<PedidoDto> Pedidos { get; set; }
        #endregion 
    }
}
