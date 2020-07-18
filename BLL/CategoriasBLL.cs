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
        public static bool Guardar(Categorias Categoria)
        {
            if (!Existe(Categoria.CategoriaId))
                return Insertar(Categoria);
            else
                return Modificar(Categoria);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Categorias.Any(c => c.CategoriaId == id);
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

        private static bool Insertar(Categorias Categoria)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Categorias.Add(Categoria);
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

        private static bool Modificar(Categorias Categoria)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(Categoria).State = EntityState.Modified;
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
            Categorias Categoria;

            try
            {
                Categoria = contexto.Categorias.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return Categoria;
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
