using ProyectoFinalServicioCliente;
using ProyectoFinalServicioCliente.UI.Consultas;
using ProyectoFinalServicioCliente.UI.rArticulo;
using ProyectoFinalServicioCliente.UI.rCategoria;
using ProyectoFinalServicioCliente.UI.rCliente;
using ProyectoFinalServicioCliente.UI.rCompras;
using ProyectoFinalServicioCliente.UI.rEventos;
using ProyectoFinalServicioCliente.UI.rFotografo;
using ProyectoFinalServicioCliente.UI.rSuplidor;
using ProyectoFinalServicioCliente.UI.rUsuarios;
using ProyectoFinalServicioCliente.UI.rVenta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ProyectoFinalServicioCliente
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            rUsuarios rUsuarios = new rUsuarios();
            rUsuarios.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            rArticulos rArticulos = new rArticulos();
            rArticulos.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            rCategoria rCategoria = new rCategoria();
            rCategoria.Show();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            rCompras rCompras = new rCompras();
            rCompras.Show();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            rSuplidor rSuplidor = new rSuplidor();
            rSuplidor.Show();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            rEventos rEventos = new rEventos();
            rEventos.Show();
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
            rClientes rClientes = new rClientes();
            rClientes.Show();
        }

        private void MenuItem_Click_8(object sender, RoutedEventArgs e)
        {
            rFotografo rFotografo = new rFotografo();
            rFotografo.Show();

        }

        private void MenuItem_Click_9(object sender, RoutedEventArgs e)
        {
            rVenta rVenta = new rVenta();
            rVenta.Show();
        }

        private void MenuItem_Click_10(object sender, RoutedEventArgs e)
        {
            cArticulos cArticulos = new cArticulos();
            cArticulos.Show();
        }

        private void MenuItem_Click_11(object sender, RoutedEventArgs e)
        {
            cCategorias cCategorias = new cCategorias();
            cCategorias.Show();
        }

        private void MenuItem_Click_12(object sender, RoutedEventArgs e)
        {
            cClientes cClientes = new cClientes();
            cClientes.Show();
        }

        private void MenuItem_Click_13(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esta venta no cumple con los requerimientos debido a que no se puede incluir el detalle en la consulta", 
                "Aviso.", MessageBoxButton.OK, MessageBoxImage.Warning);  
            cCompras cCompras = new cCompras();
            cCompras.Show();
        }
        private void MenuItem_Click_14(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esta venta no cumple con los requerimientos debido a que no se puede incluir el detalle en la consulta",
               "Aviso.", MessageBoxButton.OK, MessageBoxImage.Warning);
            cEventos cEventos = new cEventos();
            cEventos.Show();
        }
        private void MenuItem_Click_15(object sender, RoutedEventArgs e)
        {
            cFotografos cFotografos = new cFotografos();
            cFotografos.Show();
        }
        private void MenuItem_Click_16(object sender, RoutedEventArgs e)
        {
            cUsuarios cUsuarios = new cUsuarios();
            cUsuarios.Show();
        }
        private void MenuItem_Click_17(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Esta venta no cumple con los requerimientos debido a que no se puede incluir el detalle en la consulta",
               "Aviso.", MessageBoxButton.OK, MessageBoxImage.Warning);
            cVentas cVentas = new cVentas();
            cVentas.Show();
        }
    }
}
