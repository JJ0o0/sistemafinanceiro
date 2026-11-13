namespace PrjFinanceiro.Services
{
    public interface IEmprestimoService
    {
        // R1: ValorFinal = ValorBase + (ValorBase * Taxa)
        decimal CalcularJurosSimples(decimal valor, double taxa);
    }
}
