using System;
using PrjFinanceiro.Services;
using Xunit;

namespace PrjFinanceiro.Tests
{
    public class EmprestimoServiceTest
    {
        private readonly IEmprestimoService _service = new EmprestimoService();

        // ---------- R1: Juros Simples ----------

        [Fact]
        public void CalcularJurosSimples_DeveRetornarValorCorreto()
        {
            // Arrange
            decimal valorEmprestimo = 1000m;
            double taxaJuros = 0.10; // 10%
            decimal resultadoEsperado = 1100m;

            // Act
            decimal resultadoObtido = _service.CalcularJurosSimples(valorEmprestimo, taxaJuros);

            // Assert
            Assert.Equal(resultadoEsperado, resultadoObtido);
        }

        [Fact]
        public void CalcularJurosSimples_TaxaZero_DeveRetornarMesmoValor()
        {
            Assert.Equal(1000m, _service.CalcularJurosSimples(1000m, 0.0));
        }

        [Fact]
        public void CalcularJurosSimples_ValorZero_DeveRetornarZero()
        {
            Assert.Equal(0m, _service.CalcularJurosSimples(0m, 0.10));
        }

        [Fact]
        public void CalcularJurosSimples_ValorNegativo_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalcularJurosSimples(-1m, 0.10));
        }

        [Fact]
        public void CalcularJurosSimples_TaxaNegativa_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.CalcularJurosSimples(1000m, -0.10));
        }

    }
}
