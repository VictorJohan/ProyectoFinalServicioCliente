using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class ComprasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Compras compra = new Compras();
            compra.CompraId = 1;
            compra.UsuarioId = 1;
            compra.SuplidorId = 1;
            compra.Fecha = DateTime.Now;
            compra.ComprasDetalles.Add(new ComprasDetalle
            {
                CompraId = 1,
                Cantidad = 2,
                ArticuloId = 1,
                Total = 123
            });

            Assert.IsTrue(ComprasBLL.Guardar(compra));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(ComprasBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(ComprasBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(ComprasBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(ComprasBLL.GetList(c => c.CompraId == 1) != null);
        }
    }
}