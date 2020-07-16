using ProyectoFinalServicioCliente.DAL;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


namespace ProyectoFinalServicioCliente.BLL
{
    public class LoginBLL
    {
       public static bool Validar(string nombreusuario, string contrasena)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                var validar = from usuario in contexto.Usuarios
                              where usuario.NombreUsuario == nombreusuario
                              && usuario.Contrasena == Seguridad.GetEncriptado(contrasena)
                              select usuario;
                if (validar.Count() > 0)
                    paso = true;
                else
                    paso = false;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
              contexto.Dispose();
            }

            return paso;
        }

        
    }
}
