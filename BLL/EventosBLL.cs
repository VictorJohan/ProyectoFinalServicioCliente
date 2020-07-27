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
    public class EventosBLL
    {
        public static bool Guardar(Eventos evento)
        {
            if (!Existe(evento.ClienteId))
                return Insertar(evento);
            else
                return Modificar(evento);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Eventos.Any(c => c.ClienteId == id);
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

        private static bool Insertar(Eventos evento)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Eventos.Add(evento);
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

        private static bool Modificar(Eventos evento)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Database.ExecuteSqlRaw($"Delete FROM EventosDetalle Where ClienteId={evento.ClienteId}");
                foreach (var item in evento.EventosDetalles)
                {
                    contexto.Entry(item).State = EntityState.Added;
                }

                contexto.Entry(evento).State = EntityState.Modified;
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

        public static Eventos Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Eventos evento;

            try
            {
                evento = contexto.Eventos.Where(c => c.ClienteId == id).Include(c => c.EventosDetalles).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return evento;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Eventos.Find(id);
                contexto.Entry(eliminar).State = EntityState.Deleted;
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

        public static List<Eventos> GetList(Expression<Func<Eventos, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Eventos> lista = new List<Eventos>();

            try
            {
                lista = contexto.Eventos.Where(criterio).ToList();
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