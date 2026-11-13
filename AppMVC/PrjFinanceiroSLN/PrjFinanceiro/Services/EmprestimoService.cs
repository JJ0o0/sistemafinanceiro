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

        public decimal CalcularJurosCompostos(decimal valor, double taxaMensal, int meses)
        {
            ValidarEntrada(valor, taxaMensal);
            if (meses < 0)
                throw new ArgumentOutOfRangeException(nameof(meses), "O número de meses não pode ser negativo.");

            // M = P * (1 + i)^n
            double fator = Math.Pow(1 + taxaMensal, meses);
            decimal montante = valor * (decimal)fator;

            // Valor monetário: duas casas decimais
            return Math.Round(montante, 2, MidpointRounding.AwayFromZero);
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
