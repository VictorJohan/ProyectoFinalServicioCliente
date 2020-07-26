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
            //Valida que haya un Id valido en el campo ClienteId.
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El ClienteId solo puede ser de caracter numerico.", "Campo ClienteId.",
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

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();

        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (ClientesBLL.Guardar(Cliente))
            {
                Limpiar();
                MessageBox.Show("El Cliente fue registrado de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro registrar el Cliente.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El ClienteId solo puede ser de caracter numerico.", "Campo ClienteId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (ClientesBLL.Eliminar(int.Parse(ClienteIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Cliente eliminado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar el Cliente.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool Validar()
        {
            //Valida que haya un Id valido en el campo ClienteId.
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El ClienteId solo puede ser de caracter numerico.", "Campo ClienteId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que no hayan campos vacios.
            if (ClienteIdTextBox.Text.Length == 0 || NombreTextBox.Text.Length == 0 || ApellidoTextBox.Text.Length == 0 || 
                TelefonoTextBox.Text.Length == 0 || EmailTextBox.Text.Length == 0 || CelularTextBox.Text.Length == 0 || DireccionTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurese de haber llenado todos los campos.", "Campos vacios",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se haya introducido un nombre valido
            if (!Regex.IsMatch(NombreTextBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
            {
                MessageBox.Show("Solo se admiten caracteres alfabeticos.\nAsegúrese de no haber introducido espacios innecesarios.", "Nombre no valido.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se haya introducido un apelldido valido
            if (!Regex.IsMatch(ApellidoTextBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
            {
                MessageBox.Show("Solo se admiten caracteres alfabeticos.\nAsegúrese de no haber introducido espacios innecesarios.", "Apellido no valido.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida la edad del cliente
            DateTime fechaActual = DateTime.Now;
            DateTime fechaNacimiento = FechaNacimientoDatePicker.SelectedDate.Value;
            TimeSpan ts = fechaActual - fechaNacimiento;
            int edad = (int)ts.TotalDays;
            if (edad < 4015/*edad en dias*/ || edad == 0)
            {
                MessageBox.Show("La persona a la que intentas registrar es muy joven.", $"Esta persona tine {edad / 365} años.",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (edad > 65700)
            {
                MessageBox.Show("La persona a la que intentas registras tiene unas muy alta probabilades de haber fallecido.", $"Esta persona tine {edad / 365} años.",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Ayudara con la validacion del campo cedula, telefono, email, celular.
            var cliente = ClientesBLL.Buscar(int.Parse(ClienteIdTextBox.Text));

            //Valida la dirreccion de correo electronico.
            if (!Regex.IsMatch(EmailTextBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
            {
                MessageBox.Show("La direccón de correo electrónico que ha introducido no es valida.", "Campo Email.",
                   MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se le haya colocado el prefijo al telefono (ejemplo: +1).
            if (!Regex.IsMatch(TelefonoTextBox.Text, @"^(\+[0-9]{1,12})$"))
            {
                MessageBox.Show("Asegurese de haber colocado el prefijo telefonico correspondiente.", "Número de teléfono no valido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se le haya colocado el prefijo al celular no (ejemplo: +1).
            if (!Regex.IsMatch(CelularTextBox.Text, @"^(\+[0-9]{1,12})$"))
            {
                MessageBox.Show("Asegurese de haber colocado el prefijo telefonico correspondiente.", "Número celular no valido.",
                  MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Validando la longitud del telefono.
            if (TelefonoTextBox.Text.Length < 8)
            {
                MessageBox.Show("El número de teléfono no cumple con una longitud valida.", "Longitud no valida.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Validando la longitud del celular.
            if (CelularTextBox.Text.Length < 8)
            {
                MessageBox.Show("El número celular no cumple con una longitud valida.", "Longitud no valida.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            //Validando que no se repita el mismo telefono en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteTelefono(TelefonoTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteTelefono(TelefonoTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Validando que no se repita el mismo celular en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteCelular(CelularTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente el número celular.", $"El celular \"{CelularTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteCelular(CelularTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente el número celular.", $"El celular \"{CelularTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Validando que no se repita el mismo Email en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteEmail(EmailTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente el correo electrónico.", $"El Email \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteEmail(CelularTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente el correo electrónico.", $"El Email \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Validando que no se repita la misma cedula en diferentes registros.
            if (cliente != null)
            {
                if (ClientesBLL.ExisteCedula(CedulaTextBox.Text) && (cliente.Nombre != NombreTextBox.Text &&
                    cliente.Apellido != ApellidoTextBox.Text))
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente la cedula.", $"La cedula \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (ClientesBLL.ExisteCedula(CedulaTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente la cedula.", $"La cedula \"{EmailTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se haya seleccionado un sexo
            if(SexoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el sexo del cliente.", "Sexo sin seleccionar.",
                       MessageBoxButton.OK, MessageBoxImage.Information);
            }

           

            //Valida que la cedula tenga el formato adecuado
            if(CedulaTextBox.Text.Length != 0)
            {
                if (!Regex.IsMatch(CedulaTextBox.Text, @"\d{3}-\d{7}-\d{1}"))
                {
                    MessageBox.Show("Asegurece de cumplir con el siguiente formato: 111-1111111-1.", "Verifique que haya ingresado una cedula valida",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            return true;
        }
    }
}
