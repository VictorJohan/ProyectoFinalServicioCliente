using Microsoft.EntityFrameworkCore;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL
{
    public class VentasBLL
    {

        public static bool Guardar(Ventas venta)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Ventas.Add(venta);
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

        public static bool Modificar(Ventas venta)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM VentasDetalle Where VentasId={venta.VentaId}");
                foreach (var item in venta.VentasDetalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }
                contexto.Entry(venta).State = EntityState.Modified;
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

        public static Ventas Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Ventas venta;

            try
            {
                venta = contexto.Ventas.Where(v => v.VentaId == id).Include(v => v.VentasDetalles).
                    ThenInclude(v => v.Articulo).SingleOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return venta;
        }

        public static bool Eliminar(int id)
        {
            bool ok = false;
            Contexto contexto = new Contexto();

            try
            {
                var eliminar = VentasBLL.Buscar(id);
                if(eliminar != null)
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

        public static List<Ventas> GetList(Expression<Func<Ventas, bool>> criterio)
        {
            List<Ventas> Lista = new List<Ventas>();
            Contexto contexto = new Contexto();

            try
            {
                Lista = contexto.Ventas.Where(criterio).ToList();
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
    }
}