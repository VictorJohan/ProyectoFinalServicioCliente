using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class SuplidoresBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Suplidores suplidor = new Suplidores();
            suplidor.SuplidorId = 1;
            suplidor.Nombres = "Suplidor Nombres Suplidor Apellido";
            suplidor.Telefono = "+11231231234";
            suplidor.Email = "Suplidor23@gmail.com";
            suplidor.UsuarioId = 1;

            Assert.IsTrue(SuplidoresBLL.Guardar(suplidor));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(SuplidoresBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(SuplidoresBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(SuplidoresBLL.Eliminar(1));
        }

        [TestMethod()]
        public void GetSuplidoresTest()
        {
            Assert.IsTrue(SuplidoresBLL.GetSuplidores() != null);
        }

        [TestMethod()]
        public void ExisteTelefonoTest()
        {
            Assert.IsTrue(SuplidoresBLL.ExisteTelefono("+11231231234"));
        }

        [TestMethod()]
        public void ExisteEmailTest()
        {
            Assert.IsTrue(SuplidoresBLL.ExisteEmail("Suplidor23@gmail.com"));
        }
    }
}