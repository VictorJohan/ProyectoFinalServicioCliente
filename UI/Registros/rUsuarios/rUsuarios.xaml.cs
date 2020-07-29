using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
        private string confirmacion, contra;//Variables donde se encriptara la contraseña y la contraseña de confirmacion.
        public rUsuarios()
        {
            InitializeComponent();
            this.DataContext = Usuario;
        }

        //Boton que desencadenara el evento buscar para obtener un registro de la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(UsuarioIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo UsuarioId
            {
                MessageBox.Show("El Usuario Id solo puede ser de carácter numérico.", "Campo Usuario Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = UsuariosBLL.Buscar(int.Parse(UsuarioIdTextBox.Text));

            if (encontrado != null)
            {
                Usuario = encontrado;
                Usuario.Contrasena = Seguridad.DesEncriptar(encontrado.Contrasena);//Desencripta la contraseña para presentarla en el PassworBox.
                this.DataContext = Usuario;
            }
            else
            {
                MessageBox.Show("Puede ser que el usuario no exista en la base de datos.", "No se encontro el Usuario.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        //Boton que desencadenara el evento que limpiara el registro para crear uno nuevo.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //Boton que desencadenara el evento guardar en la base de datos.
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidarGuardar())
                return;

            Usuario.Contrasena = contra;//Se le asigna la contraseña encriptada a la propiedad.

            if (UsuariosBLL.Guardar(Usuario))
            {
                Limpiar();
                MessageBox.Show("El usuario fué guardado de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró guardar el usuario.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Elimina un registro de la base de datos.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(UsuarioIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Usuario Id solo puede ser de carácter numérico.", "Campo Usuario Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (UsuariosBLL.Eliminar(int.Parse(UsuarioIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Usuario eliminado.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar el usuario.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
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

        //Limpia todos los campos del registro
        public void Limpiar()
        {
            Usuario = new Usuarios();
            this.DataContext = Usuario;
            ConfirmarContraseñaPasswordBox.Clear();
        }

        //Esta funcion válida los campos del Registro de usuarios y que no se cree dos usuarios con el mismo nombre de usuarios
        public bool válidarGuardar()
        {
            //válida campo Usario Id
            if (!Regex.IsMatch(UsuarioIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Usuario Id solo puede ser de carácter numérico.", "Campo Usuario Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            //Se busca el Usuario del Id para ayudar con la válidacion de los nombres de usuario repetido
            var user = UsuariosBLL.Buscar(int.Parse(UsuarioIdTextBox.Text));

            //válida que las contraseñas se han iguales
            if (contra != confirmacion)
            {
                MessageBox.Show("Debe confirmar la contraseña para guardar cambios o crear un usuario.", "Las contraseñas no coinciden.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que todos los campos esten llenos
            if(UsuarioIdTextBox.Text.Length == 0 || NombresTextBox.Text.Length == 0 || ApellidosTextBox.Text.Length == 0 || NombreUsuarioTextBox.Text.Length == 0 || ContraseñaPasswordBox.Password.Length == 0)
            {
                MessageBox.Show("No pueden haber campos vacíos.", "Campos vacíos.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida si el Nombre de Usuario se repite en la base de datos
            if(user != null)
            {
                if (UsuariosBLL.ExisteUsuario(NombreUsuarioTextBox.Text) && user.Usuario != NombreUsuarioTextBox.Text)
                {
                    MessageBox.Show($"El Usuario \"{NombreUsuarioTextBox.Text}\" ya existe.", "Intente con otro Nombre de Usuario.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }else if (UsuariosBLL.ExisteUsuario(NombreUsuarioTextBox.Text))
            {
                MessageBox.Show($"El Usuario \"{NombreUsuarioTextBox.Text}\" ya existe.", "Intente con otro Nombre de Usuario.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
    }
}
