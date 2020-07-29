using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class VentasBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Ventas venta = new Ventas();
            venta.VentaId = 1;
            venta.UsuarioId = 1;
            venta.ClienteId = 1;
            venta.FotografoId = 1;
            venta.Fecha = DateTime.Now;
            venta.Total = 1200;
            venta.VentasDetalles.Add(new VentasDetalle
            {
                VentaId = 1,
                ArticuloId = 1,
                Cantidad = 2,
                Subtotal = 24

            });

            Assert.IsTrue(VentasBLL.Guardar(venta));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(VentasBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(VentasBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(VentasBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(VentasBLL.GetList(v => v.VentaId == 1) != null);
        }
    }
}