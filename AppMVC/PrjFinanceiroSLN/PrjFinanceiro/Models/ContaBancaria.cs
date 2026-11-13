namespace PrjFinanceiro.Models
{
    public class ContaBancaria
    {
        public string NumeroConta { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public Agencia Agencia { get; set; } = null!;
    }
}
