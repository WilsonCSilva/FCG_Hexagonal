using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class GameUpdateInput
    {
        /// <summary>
        /// Id do jogo cadastrado no banco de dados.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Nome do jogo.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        /// <summary>
        /// Produtora do jogo. 
        /// </summary>
        [Required]
        [MaxLength(100)] 
        public required string Produtora { get; set; }

        /// <summary>
        /// Descrição do jogo.
        /// </summary>
        [Required]
        [MaxLength(500)]
        public required string Descricao { get; set; }

        /// <summary>
        /// Preço do jogo. Formato: 0.00
        /// </summary>
        [Required]
        public required decimal Preco { get; set; }

        /// <summary>
        /// Data de lançamento do jogo. Formato: yyyy-MM-dd
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        public required DateTime DataLancamento { get; set; }
    }
}
