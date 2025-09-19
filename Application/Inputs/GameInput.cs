using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class GameInput
    {
        /// <summary>
        /// Nome do jogo.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        /// <summary>
        /// Produtora do jogo.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Produtora { get; set; }

        /// <summary>
        /// Descrição do jogo.  
        /// </summary>
        [Required]
        [MaxLength(500)]
        public string Descricao { get; set; }

        /// <summary>
        /// Preço do jogo. Formato: 0.00
        /// </summary>
        [Required]
        public decimal Preco { get; set; }

        /// <summary>
        /// Data de lançamento do jogo. Formato: yyyy-MM-dd
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataLancamento { get; set; }
    }
}
