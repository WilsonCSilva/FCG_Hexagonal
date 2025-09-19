namespace Domain.Models
{
    public class EntityBase
    {
        /// <summary>
        /// Nome do usuário, nome do game, etc. Máximo 200 caracteres.
        /// </summary>
        public string Nome { get; set; } = string.Empty;    

        /// <summary>
        /// Gerado automaticamente pelo banco de dados.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Data de criação do registro gerada automaticamente. Formato: yyyy-MM-dd
        /// </summary>
        public DateTime DataCriacao { get; set; }

        /// <summary>
        /// E-mail opcional, pode ser usado ou ignorado por classes derivadas.
        /// </summary>
        //public virtual string? Email { get; set; }

        public EntityBase()
        {
            DataCriacao = DateTime.Now.Date;
        }
    }
}
 