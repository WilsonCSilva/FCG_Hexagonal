using System.Text;

namespace Infra.Helpers
{
    public class CriptografiaHelper
    {
        private readonly string _key = "{5q0+aBQ-Cgjc!I&WX?]qkKSbu1t}{bcOqD88ap2BC@Cvu?.?m>fBB<lFYCH%e!t";

        public string Criptografar(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                return string.Empty;
             
            var passwordBytes = Encoding.UTF8.GetBytes(senha);
            var keyBytes = Encoding.UTF8.GetBytes(_key);  

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordBytes[i] ^= keyBytes[i % keyBytes.Length];
            }
            return Convert.ToBase64String(passwordBytes).ToString();
        } 

        public string Descriptografar(string senha)
        {
            if (string.IsNullOrEmpty(senha))
                return string.Empty;

            var passwordBytes = Convert.FromBase64String(senha);
            var keyBytes = Encoding.UTF8.GetBytes(_key);

            for (int i = 0; i < passwordBytes.Length; i++)
            {
                passwordBytes[i] ^= keyBytes[i % keyBytes.Length];
            }
            return Encoding.UTF8.GetString(passwordBytes);
        }
    }
}
