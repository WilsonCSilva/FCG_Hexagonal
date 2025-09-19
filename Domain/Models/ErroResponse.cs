namespace Domain.Models
{
    public class ErroResponse
    {
        public int StatusCode { get; set; }
        public string Erro { get; set; }
        public string Detalhe { get; set; }
        public override string ToString()
        {
            return $"StatusCode: {StatusCode} - Error: {Erro} - Detail: {Detalhe}";
        }
    }
}
 