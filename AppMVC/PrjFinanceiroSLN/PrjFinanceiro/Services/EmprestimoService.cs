using System;

namespace PrjFinanceiro.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        public decimal CalcularJurosSimples(decimal valor, double taxa)
        {
            decimal valorJuros = valor * (decimal)taxa;
            return valor + valorJuros;
        }
    }
}
