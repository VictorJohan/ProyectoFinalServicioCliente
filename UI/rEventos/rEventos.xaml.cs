using ProyectoFinalServicioCliente.Entidades;
using ProyectoFinalServicioFotografo.BLL;
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

namespace ProyectoFinalServicioCliente.UI.rEventos
{
    /// <summary>
    /// Interaction logic for rEventos.xaml
    /// </summary>
    public partial class rEventos : Window
    {
        private Eventos Evento = new Eventos();
        public rEventos()
        {
            InitializeComponent();
            this.DataContext = Evento;
            FotografoComboBox.ItemsSource = FotografosBLL.GetList();
            FotografoComboBox.SelectedValuePath = "FotografoId";
            FotografoComboBox.DisplayMemberPath = "Nombres";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
           
            var detalle = new EventosDetalle {
                Descripcion = DescripcionTextBox.Text,
                Lugar = LugarTextBox.Text,
                Fecha = (DateTime)IniciaDatePicker.SelectedDate,
                FechaVencimiento = (DateTime)VenceDatePicker.SelectedDate,
                Subtotal = double.Parse(SubtotalPrecioTextBox.Text)
            };
            
            Evento.EventosDetalles.Add(detalle);

            Cargar();
            DescripcionTextBox.Clear();
            LugarTextBox.Clear();
            IniciaDatePicker.SelectedDate = DateTime.Now;
            VenceDatePicker.SelectedDate = DateTime.Now;
            SubtotalPrecioTextBox.Clear();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            Evento.EventosDetalles.RemoveAt(DetalleDataGrid.SelectedIndex);
            Cargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {

        }


        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Evento;
        }
    }
}
