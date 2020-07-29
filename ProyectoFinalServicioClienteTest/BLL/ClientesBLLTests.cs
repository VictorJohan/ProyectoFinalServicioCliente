using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class ClientesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Clientes cliente = new Clientes();
            cliente.ClienteId = 1;
            cliente.UsuarioId = 1;
            cliente.Nombre = "Cliete Nombre";
            cliente.Apellido = "Cliente Apellido";
            cliente.Cedula = "111-1111111-1";
            cliente.Direccion = "Calle Cliente, casa cliente";
            cliente.Celular = "+11231231234";
            cliente.Telefono = "+11231231225";
            cliente.Email = "Cliente21@gmail.com";
            cliente.Sexo = "Masculino";
            cliente.FechaNacimiento = DateTime.Now;

            Assert.IsTrue(ClientesBLL.Guardar(cliente));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(ClientesBLL.Existe(1));
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(ClientesBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(ClientesBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(ClientesBLL.GetList(c => c.ClienteId == 1) != null);
        }

        [TestMethod()]
        public void ExisteEmailTest()
        {
            Assert.IsTrue(ClientesBLL.ExisteEmail("Cliente21@gmail.com"));
        }

        [TestMethod()]
        public void ExisteCelularTest()
        {
            Assert.IsTrue(ClientesBLL.ExisteCelular("+11231231234"));
        }

        [TestMethod()]
        public void ExisteTelefonoTest()
        {
            Assert.IsTrue(ClientesBLL.ExisteTelefono("+11231231225"));
        }

        [TestMethod()]
        public void ExisteCedulaTest()
        {
            Assert.IsTrue(ClientesBLL.ExisteCedula("111-1111111-1"));
        }
    }
}