using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProyectoFinalServicioFotografo.BLL
{
    public class FotografosBLL
    {

        public static bool Guardar(Fotografos fotografo)
        {
            if (!Existe(fotografo.FotografoId))
                return Insertar(fotografo);
            else
                return Modificar(fotografo);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Fotografos.Any(f => f.FotografoId == id);
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

        private static bool Insertar(Fotografos fotografo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Fotografos.Add(fotografo);
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

        private static bool Modificar(Fotografos fotografo)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(fotografo).State = EntityState.Modified;
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

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Fotografos.Find(id);
                if (eliminar != null)
                {
                    contexto.Fotografos.Remove(eliminar);
                    ok = (contexto.SaveChanges() > 0);
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

        public static Fotografos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Fotografos fotografo;

            try
            {
                fotografo = contexto.Fotografos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return fotografo;
        }

        public static List<Fotografos> GetList(Expression<Func<Fotografos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Fotografos> Lista = new List<Fotografos>();

            try
            {
                Lista = contexto.Fotografos.Where(criterio).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }

        public static List<Fotografos> GetList()
        {
            Contexto contexto = new Contexto();
            List<Fotografos> Lista = new List<Fotografos>();

            try
            {
                Lista = contexto.Fotografos.ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return Lista;
        }

        public static bool ExisteEmail(string email)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Fotografos.Any(f => f.Email.Equals(email));
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

        public static bool ExisteCelular(string celular)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Fotografos.Any(f => f.Celular.Equals(celular));
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

        public static bool ExisteTelefono(string telefono)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Fotografos.Any(f => f.Telefono.Equals(telefono));
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

        public static bool ExisteCedula(string cedula)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                ok = contexto.Fotografos.Any(f => f.Cedula.Equals(cedula));
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