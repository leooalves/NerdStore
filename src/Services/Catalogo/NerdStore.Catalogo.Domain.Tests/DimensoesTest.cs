using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NerdStore.Catalogo.Domain.Tests
{
    [TestClass]
    public class DimensoesTest
    {
        [TestMethod]
        public void Dado_dimensao_com_altura_menor_ou_igual_zero_deve_ser_invalida()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Dado_dimensao_com_largura_menor_ou_igual_zero_deve_ser_invalida()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Dado_dimensao_com_profundida_menor_ou_igual_zero_deve_ser_invalida()
        {
            Assert.Fail();
        }
    }
}
