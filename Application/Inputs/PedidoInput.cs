using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class PedidoInput
    {
        /// <summary>
        /// Id do usuário cadastrado no banco de dados.
        /// </summary>
        [Required]
        public required int UsuarioId { get; set; }

        /// <summary>
        /// Id do game cadastrado no banco de dados.
        /// </summary>
        [Required]
        public required int GameId { get; set; }
    }
}
