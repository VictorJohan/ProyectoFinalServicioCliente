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
            string[] filtro = { "Cliente Id", "Total" };
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
                            Listado = EventosBLL.GetList(e => e.Total == double.Parse(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;

                }

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