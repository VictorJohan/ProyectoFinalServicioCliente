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

namespace ProyectoFinalServicioCliente.UI.rSuplidor
{
    /// <summary>
    /// Interaction logic for rSuplidor.xaml
    /// </summary>
    public partial class rSuplidor : Window
    {
        private Suplidores Suplidor = new Suplidores();
        public rSuplidor()
        {
            InitializeComponent();
            this.DataContext = Suplidor;
        }
        
        //Evento que buscara un registro.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(SuplidorIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Suplidor Id solo puede ser de carácter numérico.", "Campo Suplidor Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = SuplidoresBLL.Buscar(int.Parse(SuplidorIdTextBox.Text));

            if(encontrado != null)
            {
                Suplidor = encontrado;
                this.DataContext = Suplidor;
            }
            else
            {
                MessageBox.Show("Puede ser que el suplidor no este registrado en la base de datos.", "No se encontro el Suplidor.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        //Evento que limpiara el WPF para un nuevo registro.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //Evento que guardara un registro.
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidar())
                return;

            if (SuplidoresBLL.Guardar(Suplidor))
            {
                Limpiar();
                MessageBox.Show("El Suplidor fué registrado de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró registrar el Suplidor.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Evento que eliminara un registro.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(SuplidorIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Suplidor Id solo puede ser de carácter numérico.", "Campo Suplidor Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (SuplidoresBLL.Eliminar(int.Parse(SuplidorIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Suplidor eliminado.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }                                     
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar el Suplidor.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }                                       
        }

        //Limpia el WPF.
        public void Limpiar()
        {   
            Suplidor = new Suplidores();
            this.DataContext = Suplidor;
        }

        //Este metodo válida los campos del WPF.
        public bool válidar()
        {
            //válida que haya un Id válido en el campo SuplidorId.
            if(!Regex.IsMatch(SuplidorIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Suplidor Id solo puede ser de carácter numérico.", "Campo Suplidor Id.", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Ayudara con la válidacion de la linea 147 y 164.
            var suplidor = SuplidoresBLL.Buscar(int.Parse(SuplidorIdTextBox.Text));

            //válida que no hayan campos vacíos.
            if(SuplidorIdTextBox.Text.Length == 0 || NombresTextBox.Text.Length == 0 || TelefonoTextBox.Text.Length == 0 ||
                EmailTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegúrese de haber llenado todos los campos.", "Campos vacíos", 
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            
            //válida la dirreccion de correo electrónico.
            if(!Regex.IsMatch(EmailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                MessageBox.Show("La direccón de correo electrónico que ha introducido no es válida.", "Campo Email.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que se le haya colocado el prefijo al telefo no (ejemplo: +1).
            if(!Regex.IsMatch(TelefonoTextBox.Text, @"^(\+[0-9]{1,12})$"))
            {
                MessageBox.Show("Asegúrese de haber colocado el prefijo telefonico correspondiente.", "Número de teléfono no válido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válidando la longitud del telefono.
            if (TelefonoTextBox.Text.Length < 8)
            {
                MessageBox.Show("El número de teléfono no cumple con una longitud válida.", "Longitud no válida.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //válidando que no se repita el mismo telefono en diferentes registros.
            if (suplidor != null)
            {
                if (SuplidoresBLL.ExisteTelefono(TelefonoTextBox.Text) && suplidor.Nombres != NombresTextBox.Text)
                {
                    MessageBox.Show("Asegúrese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (SuplidoresBLL.ExisteTelefono(TelefonoTextBox.Text))
            {
                MessageBox.Show("Asegúrese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válidando que no se repita el mismo correo en diferentes registros.
            if (suplidor != null)
            {
                if (SuplidoresBLL.ExisteEmail(EmailTextBox.Text) && suplidor.Nombres != NombresTextBox.Text)
                {
                    MessageBox.Show("Asegúrese de haber ingresado correctamente la dirección de correo electrónico.", $"El Email \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (SuplidoresBLL.ExisteEmail(EmailTextBox.Text))
            {
                MessageBox.Show("Asegúrese de haber ingresado correctamente el número de teléfono.", $"El teléfono \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }


            return true;
        }
    }
}
