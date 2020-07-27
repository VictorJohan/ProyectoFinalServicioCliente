using Microsoft.EntityFrameworkCore;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Media.Animation;

namespace ProyectoFinalServicioCliente.BLL
{
    public class ComprasBLL
    {
        public static bool Guardar(Compras compra)
        {
            if (!Existe(compra.CompraId))
                return Insertar(compra);
            else
                return Modificar(compra);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Compras.Any(c => c.CompraId == id);
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

        private static bool Insertar(Compras compra)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Compras.Add(compra);
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

        private static bool Modificar(Compras compra)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM ComprasDetalle Where CompraId={compra.CompraId}");
                foreach (var item in compra.ComprasDetalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(compra).State = EntityState.Modified;
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

        public static Compras Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Compras compra;

            try
            {
                compra = contexto.Compras.Where(c => c.CompraId == id).Include(c => c.ComprasDetalles).
                    ThenInclude(a => a.Articulo).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return compra;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Compras.Find(id);
                if (eliminar != null)
                {
                    contexto.Entry(eliminar).State = EntityState.Deleted;
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

        public static List<Compras> GetList(Expression<Func<Compras, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Compras> lista = new List<Compras>();

            try
            {
                lista = contexto.Compras.Where(criterio).ToList();
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
