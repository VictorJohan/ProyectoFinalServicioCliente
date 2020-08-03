using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
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


namespace ProyectoFinalServicioCliente.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cEventos.xaml
    /// </summary>
    public partial class cEventos : Window
    {
        public cEventos()
        {
            InitializeComponent();
            string[] filtro = { "Cliente Id", "Descripción", "Lugar","Fecha Inicio", "Fecha de Vencimiento",  "Precio", "Total" };
            FiltroComBox.ItemsSource = filtro;
        }

      
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var Listado = new List<Eventos>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0:

                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[0-9]+$"))
                        {
                            MessageBox.Show("Se esperaba un Id no una cadena de texto", "Campo Criterio",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                       Listado = EventosBLL.GetList(c => c.ClienteId == int.Parse(CriterioTexBox.Text));

                        break;
                    case 1:
                        try
                        {
                           Listado = EventosBLL.GetList(c => c.EventosDetalles.Any(d => d.Descripcion.Equals(CriterioTexBox.Text)));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {
                            Listado = EventosBLL.GetList(c => c.EventosDetalles.Any(d => d.Lugar.Equals(CriterioTexBox.Text)));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 3:
                        try
                        {

                            Listado = EventosBLL.GetList(c => c.EventosDetalles.Any(d => d.Fecha.Equals(InicioDesdeDataPicker.SelectedDate.Value)));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                    case 4:
                        try
                        {

                            Listado = EventosBLL.GetList(c => c.EventosDetalles.Any(d => d.Fecha.Equals(VenceHastaDatePicker.SelectedDate.Value)));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                    case 5:
                        try
                        {

                            Listado = EventosBLL.GetList(c => c.EventosDetalles.Any(d => d.Precio.Equals(double.Parse( CriterioTexBox.Text))));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 6:
                        try
                        {

                            Listado = EventosBLL.GetList(c => c.Total == double.Parse(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;



                }

                if (InicioDesdeDataPicker.SelectedDate != null && InicioDesdeDataPicker.SelectedDate != null) ;
                  //  Listado = Listado.Where(c => c.FechaInicio.Date >= DesdeDataPicker.SelectedDate.Value && c.FechaFin.Date <= HastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                Listado = EventosBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = Listado;
        }

       
    }
}