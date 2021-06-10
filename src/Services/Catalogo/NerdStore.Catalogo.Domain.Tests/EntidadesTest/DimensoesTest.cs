using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdStore.Catalogo.Domain.Entidades;

namespace NerdStore.Catalogo.Domain.Tests.Entidades
{
    [TestClass]
    public class DimensoesTest
    {
        [TestMethod]
        public void Dado_dimensao_com_altura_menor_ou_igual_zero_deve_ser_invalida()
        {
            var dimensoes = new Dimensoes(0,1,1);

            Assert.IsTrue(dimensoes.EhInvalida);
        }

        [TestMethod]
        public void Dado_dimensao_com_largura_menor_ou_igual_zero_deve_ser_invalida()
        {
            var dimensoes = new Dimensoes(1,0, 1);

            Assert.IsTrue(dimensoes.EhInvalida); ;
        }

        [TestMethod]
        public void Dado_dimensao_com_profundida_menor_ou_igual_zero_deve_ser_invalida()
        {
            var dimensoes = new Dimensoes(1, 1, 0);

            Assert.IsTrue(dimensoes.EhInvalida);
        }

        [TestMethod]
        public void Dado_dimensao_com_valores_maior_que_zero_deve_ser_valida()
        {
            var dimensoes = new Dimensoes(1, 1, 1);

            Assert.IsTrue(dimensoes.EhValida);
        }
    }
}
