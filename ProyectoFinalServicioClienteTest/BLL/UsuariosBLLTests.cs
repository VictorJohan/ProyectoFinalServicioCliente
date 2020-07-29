using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL.Tests
{
    [TestClass()]
    public class UsuariosBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Usuarios usuario = new Usuarios();
            usuario.UsuarioId = 1;
            usuario.Nombres = "Juan Miguel";
            usuario.Apellidos = "Perez Hernandez";
            usuario.Usuario = "JuanPerez23";
            usuario.Contrasena = Seguridad.Encriptar("123");
            usuario.Fecha = DateTime.Now;

            Assert.IsTrue(UsuariosBLL.Guardar(usuario));
        }

        [TestMethod()]
        public void ExisteTest()
        {
            Assert.IsTrue(UsuariosBLL.Existe(1));
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.IsTrue(UsuariosBLL.Buscar(1) != null);
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.IsTrue(UsuariosBLL.Eliminar(1));
        }

        [TestMethod()]
        public void AutenticarTest()
        {
            Assert.IsTrue(UsuariosBLL.Autenticar("JuanPerez23", "123"));
        }

        [TestMethod()]
        public void ExisteUsuarioTest()
        {
            Assert.IsTrue(UsuariosBLL.ExisteUsuario("JuanPerez23"));
        }
    }
}