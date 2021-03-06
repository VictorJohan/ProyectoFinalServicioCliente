﻿using Microsoft.EntityFrameworkCore;
using ProyectoFinalServicioCliente.DAL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ProyectoFinalServicioCliente.BLL
{
    public class SuplidoresBLL
    {
        public static bool Guardar(Suplidores suplidor)
        {
            if (!Existe(suplidor.SuplidorId))
                return Insertar(suplidor);
            else
                return Modificar(suplidor);
        }

        public static bool Existe(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                ok = contexto.Suplidores.Any(s => s.SuplidorId == id);
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

        private static bool Insertar(Suplidores suplidor)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Suplidores.Add(suplidor);
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

        private static bool Modificar(Suplidores suplidor)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                contexto.Entry(suplidor).State = EntityState.Modified;
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

        public static Suplidores Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Suplidores suplidor;

            try
            {
                suplidor = contexto.Suplidores.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return suplidor;
        }

        public static bool Eliminar(int id)
        {
            Contexto contexto = new Contexto();
            bool ok = false;

            try
            {
                var eliminar = contexto.Suplidores.Find(id);
                if(eliminar != null)
                {
                    contexto.Suplidores.Remove(eliminar);
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

        public static List<Suplidores> GetSuplidores()
        {
            Contexto contexto = new Contexto();
            List<Suplidores> lista = new List<Suplidores>();

            try
            {
                lista = contexto.Suplidores.ToList();
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

        public static bool ExisteTelefono(string telefono)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                var existe = from Suplidor in contexto.Suplidores
                             where Suplidor.Telefono == telefono
                             select Suplidor;
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

        public static bool ExisteEmail(string email)
        {
            Contexto contexto = new Contexto();
            bool ok;

            try
            {
                var existe = from Suplidor in contexto.Suplidores
                             where Suplidor.Email == email
                             select Suplidor;
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

        public static List<Suplidores> GetList(Expression<Func<Suplidores, bool>> criterio)
        {
            Contexto contexto = new Contexto();
            List<Suplidores> lista = new List<Suplidores>();

            try
            {
                lista = contexto.Suplidores.Where(criterio).ToList();
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
