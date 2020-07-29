using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.Entidades;
using ProyectoFinalServicioFotografo.BLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioFotografo.BLL.Tests
{
    [TestClass()]
    public class FotografosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Fotografos fotografo = new Fotografos();
            fotografo.FotografoId = 1;
            fotografo.UsuarioId = 1;
            fotografo.Nombres = "Fotografo Nombre Fotografo Apellido";
            fotografo.Cedula = "111-1111111-1";
            fotografo.Direccion = "Calle Fotografo, casa Fotografo";
            fotografo.Celular = "+11231231234";
            fotografo.Telefono = "+11231231225";
            fotografo.Email = "Fotografo21@gmail.com";
            fotografo.Sexo = "Masculino";
            fotografo.FechaNacimiento = DateTime.Now;

            Assert.IsTrue(FotografosBLL.Guardar(fotografo));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(FotografosBLL.Existe(1));
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(FotografosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(FotografosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.IsTrue(FotografosBLL.GetList() != null);
        }

        [TestMethod()]
        public void GetListTest1()
        {
            Assert.IsTrue(FotografosBLL.GetList() != null);
        }

        [TestMethod()]
        public void ExisteEmailTest()
        {
            Assert.IsTrue(FotografosBLL.ExisteEmail("Fotografo21@gmail.com"));
        }

        [TestMethod()]
        public void ExisteCelularTest()
        {
            Assert.IsTrue(FotografosBLL.ExisteCelular("+11231231234"));
        }

        [TestMethod()]
        public void ExisteTelefonoTest()
        {
            Assert.IsTrue(FotografosBLL.ExisteTelefono("+11231231225"));
        }

        [TestMethod()]
        public void ExisteCedulaTest()
        {
            Assert.IsTrue(FotografosBLL.ExisteCedula("111-1111111-1"));
        }
    }
}