using System;

namespace PrjFinanceiro.Services
{
    public class EmprestimoService : IEmprestimoService
    {
        public decimal CalcularJurosSimples(decimal valor, double taxa)
        {
            ValidarEntrada(valor, taxa);

            decimal valorJuros = valor * (decimal)taxa;
            return valor + valorJuros;
        }

        private static void ValidarEntrada(decimal valor, double taxa)
        {
            if (valor < 0)
                throw new ArgumentOutOfRangeException(nameof(valor), "O valor não pode ser negativo.");
            if (taxa < 0)
                throw new ArgumentOutOfRangeException(nameof(taxa), "A taxa não pode ser negativa.");
        }
    }
}