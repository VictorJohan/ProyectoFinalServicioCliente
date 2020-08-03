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

namespace ProyectoFinalServicioCliente.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cVentas.xaml
    /// </summary>
    public partial class cVentas : Window
    {
        public cVentas()
        {
            InitializeComponent();
            string[] filtro = { "Venta Id", "Cliente Id", "Fotografo Id", "Fecha", "Total" };
            FiltroComBox.ItemsSource = filtro;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Ventas>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {

                    case 0:
                        try
                        {
                            listado = VentasBLL.GetList(c => c.VentaId == int.Parse(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 1:
                        try
                        {
                            listado = VentasBLL.GetList(c => c.ClienteId == int.Parse(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {
                            listado = VentasBLL.GetList(c => c.FotografoId == int.Parse(CriterioTexBox.Text));
                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    

                }
            }
            else
            {
                if (DesdeDataPicker.SelectedDate != null)
                    listado = VentasBLL.GetList(c => c.Fecha.Date >= DesdeDataPicker.SelectedDate);

                if (HastaDatePicker.SelectedDate != null)
                    listado = VentasBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);


                listado = VentasBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = listado;
        }


    }
}

