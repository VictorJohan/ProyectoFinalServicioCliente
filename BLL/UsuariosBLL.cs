using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL
{
    public class UsuariosBLL 
    {
        public static bool Guardar(Usuarios usuario)
        {
            if (!Existe(usuario.UsuarioId))
                return Insertar(usuario);
            else
                return Modificar(usuario);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Usuarios.Any(u => u.UsuarioId == id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Insertar(Usuarios usuario)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Usuarios.Add(usuario);
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        private static bool Modificar(Usuarios usuarios)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(usuarios).State = EntityState.Modified;
                ok = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static Usuarios Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Usuarios usuario;

            try
            {
                usuario = contexto.Usuarios.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return usuario;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var usuario = contexto.Usuarios.Find(id);
                if (usuario != null)
                {
                    contexto.Usuarios.Remove(usuario);
                    ok = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static bool Validar(string nombreusuario, string contrasena)
        {
            bool ok = false;
            Contexto contexto = new Contexto();

            try
            {
                var validar = from usuario in contexto.Usuarios
                              where usuario.Usuario == nombreusuario
                              && usuario.Contrasena == Seguridad.Encriptar(contrasena)
                              select usuario;
                if (validar.Count() > 0)
                    ok = true;
                else
                    ok = false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }

        public static bool ExisteUsuario(string user)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                var existe = from usuario in contexto.Usuarios
                     where usuario.Usuario == user select usuario;
                if (existe.Count() > 0)
                    ok = true;
                else
                    ok = false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return ok;
        }
    }
}
