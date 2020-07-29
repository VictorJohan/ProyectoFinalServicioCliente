using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class EventosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Eventos evento = new Eventos();
            evento.ClienteId = 1;
            evento.UsuarioId = 1;
            evento.Total = 430;
            evento.EventosDetalles.Add(new EventosDetalle
            {
                ClienteId = 1,
                Descripcion = "Descripcion",
                Lugar = "Lugar",
                Fecha = DateTime.Now,
                FechaVencimiento = DateTime.Now,
                Precio = 1200,
                FotografoId = 1
            });

            Assert.IsTrue(EventosBLL.Guardar(evento));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(EventosBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(EventosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(EventosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(EventosBLL.GetList(e => e.ClienteId == 1) != null);
        }
    }
}