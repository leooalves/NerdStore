using Microsoft.VisualStudio.TestTools.UnitTesting;
using NerdStore.Catalogo.Domain.Entidades;

namespace NerdStore.Catalogo.Domain.Tests.Entidades
{
    [TestClass]
    public class CategoriaTest
    {
        [TestMethod]
        public void Dado_categoria_com_nome_vazio_deve_ser_invalida()
        {
            var categoria = new Categoria(string.Empty,100);

            Assert.IsTrue(categoria.EhInvalido);
        }

        [TestMethod]
        public void Dado_categoria_com_codigo_menor_ou_igual_zero_deve_ser_invalida()
        {
            var categoria = new Categoria("Nome da Categoria", 0);

            Assert.IsTrue(categoria.EhInvalido);
        }

        [TestMethod]
        public void Dado_categoria_valida()
        {
            var categoria = new Categoria("Nome da Categoria", 100);

            Assert.IsTrue(categoria.EhValido);
        }

    }
}
