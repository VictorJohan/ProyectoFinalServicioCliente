using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using ProyectoFinalServicioFotografo.BLL;
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

namespace ProyectoFinalServicioCliente.UI.rFotografo
{
    /// <summary>
    /// Interaction logic for rFografo.xaml
    /// </summary>
    public partial class rFotografo : Window
    {
        Fotografos Fotografo = new Fotografos();
        public rFotografo()
        {
            InitializeComponent();
            this.DataContext = Fotografo;
            SexoComboBox.Items.Add("Femenino");
            SexoComboBox.Items.Add("Masculino");
        }
        //Metodo limpiar.
        private void Limpiar()
        {
            Fotografo = new Fotografos();
            this.DataContext = Fotografo;
        }

        //Este evento busca un registro en la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            //Valida que haya un Id valido en el campo ClienteId.
            if (!Regex.IsMatch(FotografoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El ClienteId solo puede ser de caracter numerico.", "Campo ClienteId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = FotografosBLL.Buscar(int.Parse(FotografoIdTextBox.Text));

            if (encontrado != null)
            {
                Fotografo = encontrado;
                this.DataContext = Fotografo;
            }
            else
            {
                MessageBox.Show("Puede ser que el Fotografo no este registrado en la base de datos.", "No se encontro el Fotografo.", MessageBoxButton.OK,
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
            if (!Validar())
                return;

            if (FotografosBLL.Guardar(Fotografo))
            {
                Limpiar();
                MessageBox.Show("El Fotografo fue registrado de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro registrar el Fotografo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Este evento elimina un registro de la base de datos.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(FotografoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El ClienteId solo puede ser de caracter numerico.", "Campo ClienteId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            if (FotografosBLL.Eliminar(int.Parse(FotografoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Fotografo eliminado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar el Fotografo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Este evento valida todos los campos.
        public bool Validar()//todo: corregir la validacion de sueldo para que tome numeros grandes. Y revisar la validacion de la existencia email
        {
            //Valida que haya un Id valido en el campo ClienteId.
            if (!Regex.IsMatch(FotografoIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El ClienteId solo puede ser de caracter numerico.", "Campo ClienteId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que no hayan campos vacios.
            if (FotografoIdTextBox.Text.Length == 0 || NombresTextBox.Text.Length == 0 || CedulaTextBox.Text.Length == 0 ||
                TelefonoTextBox.Text.Length == 0 || EmailTextBox.Text.Length == 0 || CelularTextBox.Text.Length == 0 ||
                DireccionTextBox.Text.Length == 0 || SueldoTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurese de haber llenado todos los campos.", "Campos vacios",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se haya introducido un nombre valido
            if (!Regex.IsMatch(NombresTextBox.Text, "^[a-zA-Z'.\\s]{1,40}$"))
            {
                MessageBox.Show("Solo se admiten caracteres alfabeticos.\nAsegúrese de no haber introducido espacios innecesarios.", "Nombre no valido.",
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

            //Ayudara con la validacion del campo cedula, telefono, email, celular.
            var fotografo = ClientesBLL.Buscar(int.Parse(FotografoIdTextBox.Text));

            //Validando que no se repita el mismo telefono en diferentes registros.
            if (fotografo != null)
            {
                if (FotografosBLL.ExisteTelefono(TelefonoTextBox.Text) && fotografo.Nombre != NombresTextBox.Text)
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (FotografosBLL.ExisteTelefono(TelefonoTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente el número de teléfono.", $"El teléfono \"{TelefonoTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Validando que no se repita el mismo celular en diferentes registros.
            if (fotografo != null)
            {
                if (FotografosBLL.ExisteCelular(CelularTextBox.Text) && fotografo.Nombre != NombresTextBox.Text)
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente el número celular.", $"El celular \"{CelularTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (FotografosBLL.ExisteCelular(CelularTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente el número celular.", $"El celular \"{CelularTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Validando que no se repita el mismo Email en diferentes registros.
            if (fotografo != null)
            {
                if (FotografosBLL.ExisteEmail(EmailTextBox.Text) && fotografo.Nombre != NombresTextBox.Text)
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
            if (fotografo != null)
            {
                if (FotografosBLL.ExisteCedula(CedulaTextBox.Text) && fotografo.Nombre != NombresTextBox.Text)
                {
                    MessageBox.Show("Asegurese que haya ingresado correctamente la cedula.", $"La cedula \"{CedulaTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            else if (FotografosBLL.ExisteCedula(CedulaTextBox.Text))
            {
                MessageBox.Show("Asegurese que haya ingresado correctamente la cedula.", $"La cedula \"{CedulaTextBox.Text}\" ya existe.",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que se haya seleccionado un sexo
            if (SexoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el sexo del fotografo.", "Sexo sin seleccionar.",
                       MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            //Valida que la cedula tenga el formato adecuado
            if (CedulaTextBox.Text.Length != 0)
            {
                if (!Regex.IsMatch(CedulaTextBox.Text, @"\d{3}-\d{7}-\d{1}"))
                {
                    MessageBox.Show("Asegurece de cumplir con el siguiente formato: 111-1111111-1.", "Verifique que haya ingresado una cedula valida",
                      MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }

            //Valida que se ingrese un sueldo valido
            if (!Regex.IsMatch(SueldoTextBox.Text, @"^[0-9]{1,3}$|^[0-9]{1,3}\.[0-9]{1,3}$"))
            {
                MessageBox.Show("En el campo sueldo solo pueden haber caracteres numericos.", "Campo Sueldo.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}