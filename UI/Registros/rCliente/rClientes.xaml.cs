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

namespace ProyectoFinalServicioCliente.UI.rCliente
{
    /// <summary>
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        Clientes Cliente = new Clientes();
        public rClientes()
        {
            InitializeComponent();
            this.DataContext = Cliente;
            SexoComboBox.Items.Add("Femenino");
            SexoComboBox.Items.Add("Masculino");
        }

        private void Limpiar()
        {
            Cliente = new Clientes();
            this.DataContext = Cliente;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            //válida que haya un Id válido en el campo ClienteId.
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Cliente Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = ClientesBLL.Buscar(int.Parse(ClienteIdTextBox.Text));

            if(encontrado != null)
            {
                Cliente = encontrado;
                this.DataContext = Cliente;
            }
            else
            {
                MessageBox.Show("Puede ser que el cliente no este registrado en la base de datos.", "No se encontro el Cliente.", MessageBoxButton.OK,
                   MessageBoxImage.Information);
            }
        }

        //Este evento limpia los campos para un nuevo registro.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();

        }

        //Este evento guarda un registro en la base de datos.
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidar())
                return;

            if (ClientesBLL.Guardar(Cliente))
            {
                Limpiar();
                MessageBox.Show("El Cliente fué registrado de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró registrar el Cliente.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Este evento elimina un registro de la base de datos
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Cliente Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (ClientesBLL.Eliminar(int.Parse(ClienteIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Cliente eliminado.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar el Cliente.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Este metodo válida todos los campos.
        public bool válidar()
        {
            //válida que haya un Id válido en el campo ClienteId.
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Cliente Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que no hayan campos vacíos.
            if (ClienteIdTextBox.Text.Length == 0 || NombreTextBox.Text.Length == 0 || ApellidoTextBox.Text.Length == 0 ||
                EmailTextBox.Text.Length == 0 || CelularTextBox.Text.Length == 0 || DireccionTextBox.Text.Length == 0 ||
                CedulaTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegúrese de haber llenado todos los campos.", "Campos vacíos",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que se haya introducido un nombre válido
            if (!Regex.IsMatch(NombreTextBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
            {
                MessageBox.Show("Solo se admiten carácteres alfabeticos.\nAsegúrese de no haber introducido espacios innecesarios.", "Nombre no válido.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que se haya introducido un apelldido válido
            if (!Regex.IsMatch(ApellidoTextBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
            {
                MessageBox.Show("Solo se admiten carácteres alfabeticos.\nAsegúrese de no haber introducido espacios innecesarios.", "Apellido no válido.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida la edad del cliente
            DateTime fechaActual = DateTime.Now;
            DateTime fechaNacimiento = FechaNacimientoDatePicker.SelectedDate.Value;
            TimeSpan ts = fechaActual - fechaNacimiento;
            int edad = (int)ts.TotalDays;
            if (edad < 4015/*edad en dias*/ || edad == 0)
            {
                MessageBox.Show("La persona a la que intentas registrar es muy jóven.", $"Esta persona tine {edad / 365} años.",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (edad > 65700)
            {
                MessageBox.Show("La persona a la que intentas registras tiene unas muy alta probabilades de haber fallecido.", $"Esta persona tine {edad / 365} años.",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Ayudara con la válidacion del campo cedula, telefono, email, celular.
            var cliente = ClientesBLL.Buscar(int.Parse(ClienteIdTextBox.Text));

            //válida la dirreccion de correo electrónico.
            if (!Regex.IsMatch(EmailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                MessageBox.Show("La direccón de correo electrónico que ha introducido no es válida.", "Campo Email.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que se le haya colocado el prefijo al telefono (ejemplo: +1).
            if (TelefonoTextBox.Text.Length != 0 && !Regex.IsMatch(TelefonoTextBox.Text, @"^(\+[0-9]{1,12})$"))
            {
                MessageBox.Show("Asegúrese de haber colocado el prefijo telefonico correspondiente.", "Número de teléfono no válido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que se le haya colocado el prefijo al celular no (ejemplo: +1).
            if (!Regex.IsMatch(CelularTextBox.Text, @"^(\+[0-9]{1,12})$"))
            {
                MessageBox.Show("Asegúrese de haber colocado el prefijo telefonico correspondiente.", "Número celular no válido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válidando la longitud del telefono.
            if (TelefonoTextBox.Text.Length != 0 && TelefonoTextBox.Text.Length < 8)
            {
                MessageBox.Show("El número de teléfono no cumple con una longitud válida.", "Longitud no válida.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //válidando la longitud del celular.
            if (CelularTextBox.Text.Length < 8)
            {
                MessageBox.Show("El número celular no cumple con una longitud válida.", "Longitud no válida.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //válidando que no se repita el mismo telefono en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteTelefono(TelefonoTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegúrese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteTelefono(TelefonoTextBox.Text))
            {
                MessageBox.Show("Asegúrese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válidando que no se repita el mismo celular en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteCelular(CelularTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegúrese que haya ingresado correctamente el número celular.", $"El celular \"{CelularTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteCelular(CelularTextBox.Text))
            {
                MessageBox.Show("Asegúrese que haya ingresado correctamente el número celular.", $"El celular \"{CelularTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válidando que no se repita el mismo Email en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteEmail(EmailTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegúrese que haya ingresado correctamente el correo electrónico.", $"El Email \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteEmail(CelularTextBox.Text))
            {
                MessageBox.Show("Asegúrese que haya ingresado correctamente el correo electrónico.", $"El Email \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válidando que no se repita la misma cedula en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteCedula(CedulaTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegúrese que haya ingresado correctamente la cédula.", $"La cédula \"{CedulaTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteCedula(CedulaTextBox.Text))
            {
                MessageBox.Show("Asegúrese que haya ingresado correctamente la cédula.", $"La cédula \"{CedulaTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que se haya seleccionado un sexo
            if(SexoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el sexo del cliente.", "Sexo sin seleccionar.",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //válida que la cedula tenga el formato adecuado
            if(CedulaTextBox.Text.Length != 0)
            {
                if (!Regex.IsMatch(CedulaTextBox.Text, @"\d{3}-\d{7}-\d{1}"))
                {
                    MessageBox.Show("Asegúrese de cumplir con el siguiente formato: 111-1111111-1.", "Verifique que haya ingresado una cédula válida",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }
    }
}
