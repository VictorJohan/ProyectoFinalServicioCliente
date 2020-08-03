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

namespace ProyectoFinalServicioCliente.UI.Consultas.mConsultas
{
    /// <summary>
    /// Interaction logic for mConsultas.xaml
    /// </summary>
    public partial class mConsultas : Window
    {
        public mConsultas()
        {
            InitializeComponent();
        }

        //Consulta Menu-------------------------------------------------------------------------
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            cArticulos cArticulos = new cArticulos();
            cArticulos.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            cCategorias cCategoria = new cCategorias();
            cCategoria.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            cClientes cClientes = new cClientes();
            cClientes.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            cCompras cCompras = new cCompras();
            cCompras.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            cEventos cEventos = new cEventos();
            cEventos.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {

            cFotografos cFotografo = new cFotografos();
            cFotografo.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            cUsuarios cUsuarios = new cUsuarios();
            cUsuarios.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            cVentas cVenta = new cVentas();
            cVenta.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            cUsuarios cUsuarios = new cUsuarios();
            cUsuarios.Show();
        }
        //Consulta Menu--------------------------------------------------------------------------

        //Consulta Button--------------------------------------------------------------------------
        private void ConsultaArticulosButton_Click(object sender, RoutedEventArgs e)
        {
            cArticulos cArticulos = new cArticulos();
            cArticulos.Show();
        }

        private void ConsultaCategoriaButton_Click(object sender, RoutedEventArgs e)
        {
            cCategorias cCategoria = new cCategorias();
            cCategoria.Show();
        }

        private void ConsultaCompraButton_Click(object sender, RoutedEventArgs e)
        {
            cCompras cCompras = new cCompras();
            cCompras.Show();
        }

        private void ConsultaSuplidorButton_Click(object sender, RoutedEventArgs e)
        {
            cSuplidor cSuplidor = new cSuplidor();
            cSuplidor.Show();
        }

        private void ConsultaEventoButton_Click(object sender, RoutedEventArgs e)
        {
            cEventos cEventos = new cEventos();
            cEventos.Show();
        }

        private void ConsultaClienteButton_Click(object sender, RoutedEventArgs e)
        {
            cClientes cClientes = new cClientes();
            cClientes.Show();
        }

        private void ConsultaFotografoButton_Click(object sender, RoutedEventArgs e)
        {
            cFotografos cFotografo = new cFotografos();
            cFotografo.Show();
        }

        private void ConsultaVentaButton_Click(object sender, RoutedEventArgs e)
        {
            cVentas cVenta = new cVentas();
            cVenta.Show();
        }

        //Consulta Button--------------------------------------------------------------------------
    }
}
