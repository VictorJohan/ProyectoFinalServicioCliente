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

namespace ProyectoFinalServicioCliente.UI.rCliente
{
    /// <summary>
    /// Interaction logic for rClientes.xaml
    /// </summary>
    public partial class rClientes : Window
    {
        Clientes cliente = new Clientes();
        public rClientes()
        {
            InitializeComponent();
            this.DataContext = cliente;
            SexoComboBox.Items.Add("Femenino");
            SexoComboBox.Items.Add("Masculino");
            ClienteIdTextBox.Text = "0";
            UsuarioIdTextBox.Text = "0";
        }

        private void Limpiar()
        {
            ClienteIdTextBox.Text = "0";
            UsuarioIdTextBox.Text = "0";
            NombresTextBox.Text = string.Empty;
            ApellidoTextBox.Text = string.Empty;
            FechaNaciDatePicker.SelectedDate = DateTime.Now;
            SexoComboBox.SelectedItem = null;
            CedulaTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CelularTextBox.Text = string.Empty;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private bool ExisteEnBaseDatos()
        {
            Clientes clientes = ClientesBLL.Buscar(cliente.ClienteId);
            return (clientes != null);
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Convert.ToInt32(ClienteIdTextBox.Text) == 0)
                paso = ClientesBLL.Guardar(cliente);
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("No se puede modificar un cliente que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    paso = ClientesBLL.Modificar(cliente);
                }
            }
            if (paso)
                Limpiar();
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Clientes clienteAnterior = ClientesBLL.Buscar(Convert.ToInt32(ClienteIdTextBox.Text));

            if (clienteAnterior != null)
            {
                cliente = clienteAnterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("Cliente no encontrado");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(Convert.ToInt32(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("Cliente eliminado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se puede eliminar un cliente que no existe");
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = cliente;
        }
    }
}
