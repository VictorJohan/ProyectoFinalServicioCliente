﻿using Microsoft.EntityFrameworkCore;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;

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
                ok = contexto.Eventos.Any(e => e.ClienteId == id);
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
                contexto.Database.ExecuteSqlRaw($"Delete FROM EventosDetalle where ClienteId={evento.ClienteId}");
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
                evento = contexto.Eventos.Where(e => e.ClienteId == id).Include(d => d.EventosDetalles).
                    ThenInclude(f => f.Fotografo).SingleOrDefault();
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

        public static List<Eventos> GetList(Expression<Func<Eventos, bool>> eventos)
        {
            Contexto db = new Contexto();
            List<Eventos> listado = new List<Eventos>();

            try
            {
                listado = db.Eventos.Where(eventos).ToList();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return listado;
        }

      
    }
}