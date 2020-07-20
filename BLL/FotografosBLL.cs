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
    public class FotografosBLL
    {
        public static bool Guardar(Fotografos fotografo)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Fotografos.Add(fotografo) != null)
                    paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Fotografos fotografo)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(fotografo).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Fotografos.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Fotografos Buscar(int id)
        {
            Fotografos fotografo = new Fotografos();
            Contexto db = new Contexto();

            try
            {
                fotografo = db.Fotografos.Find(id);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return fotografo;
        }

        public static List<Fotografos> GetList(Expression<Func<Fotografos, bool>> fotografo)
        {
            List<Fotografos> Lista = new List<Fotografos>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Fotografos.Where(fotografo).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }
    }
}