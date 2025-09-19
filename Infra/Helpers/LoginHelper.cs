namespace Infra.Helpers
{
    public class LoginHelper
    {

        /// <summary>
        /// Verifica se o email informado pelo usuário é igual ao email cadastrado no banco de dados.
        /// </summary>
        /// <param name="emailLogin"></param>
        /// <param name="emailBanco"></param>
        /// <returns></returns>
        public Boolean VerificarCadastroEmail(string emailLogin, string emailBanco)
        {
            if (emailLogin == emailBanco)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        /// <summary>
        /// Verifica se a senha informada pelo usuário é igual a senha cadastrada no banco de dados.
        /// </summary>
        /// <param name="senhaLogin"></param>
        /// <param name="senhaBanco"></param>
        /// <returns></returns>
        public Boolean VerificarCadastroSenha(string senhaLogin, string senhaBanco)
        {
            if (senhaLogin == senhaBanco)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
