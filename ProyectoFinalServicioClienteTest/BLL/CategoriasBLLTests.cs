using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class CategoriasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Categorias categoria = new Categorias();
            categoria.CategoriaId = 1;
            categoria.Nombre = "Papel";

            Assert.IsTrue(CategoriasBLL.Guardar(categoria));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(CategoriasBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(CategoriasBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(CategoriasBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListCategoriasTest()
        {
            Assert.IsTrue(CategoriasBLL.GetListCategorias() != null);
        }
    }
}