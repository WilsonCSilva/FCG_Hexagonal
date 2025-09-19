using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class PedidoUpdateInput
    {
        /// <summary>
        /// Id do pedido cadastrado no banco de dados.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Id do game cadastrado no banco de dados.
        /// </summary>
        [Required]
        public required int GameId { get; set; }
    }
}
