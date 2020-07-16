using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProyectoFinalServicioCliente.UI.rUsuarios
{
    /// <summary>
    /// Interaction logic for rUsuarios.xaml
    /// </summary>
    public partial class rUsuarios : Window
    {
        Usuarios Usuario = new Usuarios();
        private string confirmacion, contra;
        public rUsuarios()
        {
            InitializeComponent();
            this.DataContext = Usuario;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = UsuariosBLL.Buscar(int.Parse(UsuarioIdTextBox.Text));

            if (encontrado != null)
            {
                Usuario = encontrado;
                Usuario.Contrasena = Seguridad.DesEncriptar(encontrado.Contrasena);
                this.DataContext = Usuario;
            }
            else
            {
                MessageBox.Show("Puede ser que el usuario no exista en la base de datos.", "No se encontro el Usuario.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarGuardar())
                return;

            Usuario.Contrasena = contra;

            if (UsuariosBLL.Guardar(Usuario))
            {
                MessageBox.Show(contra);
                Limpiar();
                MessageBox.Show("El usuario fue guardado de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro guardar el usuario.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsuariosBLL.Eliminar(int.Parse(UsuarioIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Usuario eliminado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar el usuario.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        //Encripta la contraseña a medida que se va digitanto
        private void ContraseñaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            contra = Seguridad.Encriptar(ContraseñaPasswordBox.Password);
        }

        //Encripta la confirmacion de la contraseña a medida que se va digitanto
        private void ConfirmarContraseñaPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            confirmacion = Seguridad.Encriptar(ConfirmarContraseñaPasswordBox.Password);
        }

        public void Limpiar()
        {
            Usuario = new Usuarios();
            this.DataContext = Usuario;
            ConfirmarContraseñaPasswordBox.Clear();
        }

        public bool ValidarGuardar()
        {
            if (contra != confirmacion)
            {
                MessageBox.Show("Debe confirmar la contraseña para guardar cambios o crear un usuario.", "Las contraseñas no coinciden.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }


            return true;
        }
    }
}
