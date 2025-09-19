using System.ComponentModel.DataAnnotations;

namespace Core.Inputs
{
    public class UsuarioUpdateInput
    {
        /// <summary>
        /// Id do usuário no banco de dados.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome completo.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }

        /// <summary>
        /// E-mail que será utilizado no login.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Obrigatório letra maiúscula, minúscula, número e caracter especial.
        /// </summary>
        [Required] 
        [MaxLength(50)]
        [MinLength(8)]
        public string Senha { get; set; }

        /// <summary>
        /// Endereço completo.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public required string Endereco { get; set; }

        /// <summary>
        /// Formato: yyyy-MM-dd
        /// </summary>
        [DataType(DataType.Date)]
        public required DateTime DataNascimento { get; set; }

        /// <summary>
        /// Tipo de usuário: 1 = Administrador, 2 = Usuário comum.
        /// </summary>
        public required byte TipoUsuario { get; set; }
    }
}
