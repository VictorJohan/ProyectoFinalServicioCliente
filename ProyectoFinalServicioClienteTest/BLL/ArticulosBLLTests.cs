using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class ArticulosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Articulos articulo = new Articulos();

            articulo.ArticuloId = 1;
            articulo.Descripcion = "Hoja";
            articulo.CategoriaId = 1;
            articulo.UsuarioId = 1;
            articulo.Stock = 12;
            articulo.Precio = 23;
            articulo.Costo = 10;
            articulo.Ganancia = 13;

            Assert.IsTrue(ArticulosBLL.Guardar(articulo));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(ArticulosBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(ArticulosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(ArticulosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListArticulosTest()
        {
            Assert.IsTrue(ArticulosBLL.GetListArticulos() != null);
        }
    }
}