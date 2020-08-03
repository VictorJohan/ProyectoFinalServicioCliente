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
    /// Interaction logic for cCompras.xaml
    /// </summary>
    public partial class cCompras : Window
    {
        public cCompras()
        {
            InitializeComponent();
            string[] filtro = { "Id", "Monto", "Fecha", "SuplidorId" };
            FiltroComBox.ItemsSource = filtro;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var Listado = new List<Compras>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0:
                        //todo: poner esto en los Id de las consulatas
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[0-9]+$"))
                        {
                            MessageBox.Show("Se esperaba un Id no una cadena de texto", "Campo Criterio",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                        Listado = ComprasBLL.GetList(c => c.CompraId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        try
                        {
                            Listado = ComprasBLL.GetList(c => c.Monto == double.Parse(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {

                            if (DesdeDataPicker.SelectedDate != null)
                                Listado = ComprasBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

                            if (HastaDatePicker.SelectedDate != null)
                                Listado = ComprasBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

                            Listado = ComprasBLL.GetList(C => C.Fecha == DesdeDataPicker.SelectedDate.Value);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 3:
                        try
                        {                           
                            Listado = ComprasBLL.GetList(c => c.SuplidorId == int.Parse(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                }

                if (DesdeDataPicker.SelectedDate != null && HastaDatePicker.SelectedDate != null)
                    Listado = Listado.Where(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate.Value && c.Fecha.Date <= HastaDatePicker.SelectedDate.Value).ToList();

            }


            else
            {
                Listado = ComprasBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = Listado;
        }

      
    }
}