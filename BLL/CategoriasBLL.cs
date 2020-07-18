using Microsoft.EntityFrameworkCore;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL
{
    public class CategoriasBLL
    {
        public static bool Guardar(Categorias categoria)
        {
            if (!Existe(categoria.CategoriaId))
                return Insertar(categoria);
            else
                return Modificar(categoria);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Categorias.Any(a => a.CategoriaId == id);
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

        private static bool Insertar(Categorias categoria)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Categorias.Add(categoria);
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

        private static bool Modificar(Categorias categoria)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(categoria).State = EntityState.Modified;
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

        public static Categorias Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Categorias Categorias;

            try
            {
                Categorias = contexto.Categorias.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Categorias;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Categorias.Find(id);
                if (eliminar != null)
                {
                    contexto.Categorias.Remove(eliminar);
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

        public static List<Categorias> GetListCategorias()
        {
            Contexto contexto = new Contexto();
            List<Categorias> lista = new List<Categorias>();

            try
            {
                lista = contexto.Categorias.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return lista;
        }
    }
}
