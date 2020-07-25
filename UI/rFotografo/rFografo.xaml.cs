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

namespace ProyectoFinalServicioCliente.UI.rFotografo
{
    /// <summary>
    /// Interaction logic for rFografo.xaml
    /// </summary>
    public partial class rFografo : Window
    {
        Fotografos fotografo = new Fotografos();
        public rFografo()
        {
            InitializeComponent();
            this.DataContext = fotografo;
            FotografoIdTextBox.Text = "0";
            UsuarioIdTextBox.Text = "0";
            SueldoTextBox.Text = "0";
            SexoComboBox.Items.Add("Femenino");
            SexoComboBox.Items.Add("Masculino");
        }
        private void Limpiar()
        {
            FotografoIdTextBox.Text = "0";
            UsuarioIdTextBox.Text = "0";
            NombreTextBox.Text = string.Empty;
            FechaNaciDatePicker.SelectedDate = DateTime.Now;
            SexoComboBox.SelectedItem = null;
            CedulaTextBox.Text = string.Empty;
            DireccionTextBox.Text = string.Empty;
            TelefonoTextBox.Text = string.Empty;
            CelularTextBox.Text = string.Empty;
            SueldoTextBox.Text = "0";
        }
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        private bool ExisteEnBaseDatos()
        {
            Fotografos fotografos = FotografosBLL.Buscar(fotografo.FotografoId);
            return (fotografos != null);
        }
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Convert.ToInt32(FotografoIdTextBox.Text) == 0)
                paso = FotografosBLL.Guardar(fotografo);
            else
            {
                if (!ExisteEnBaseDatos())
                {
                    MessageBox.Show("No se puede modificar un fotografo que no existe", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    paso = FotografosBLL.Modificar(fotografo);
                }
            }
            if (paso)
                Limpiar();
        }
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Fotografos fotografoAnterior = FotografosBLL.Buscar(Convert.ToInt32(FotografoIdTextBox.Text));

            if (fotografoAnterior != null)
            {
                fotografo = fotografoAnterior;
                Actualizar();
            }
            else
            {
                MessageBox.Show("Fotografo no encontrado");
                Limpiar();
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientesBLL.Eliminar(Convert.ToInt32(FotografoIdTextBox.Text)))
            {
                MessageBox.Show("Fotografo eliminado");
                Limpiar();
            }
            else
            {
                MessageBox.Show("No se puede eliminar un Fotografo que no existe");
            }
        }

        private void Actualizar()
        {
            this.DataContext = null;
            this.DataContext = fotografo;
        }
    }
}