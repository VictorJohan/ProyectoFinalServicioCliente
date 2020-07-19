using ProyectoFinalServicioCliente.UI.Login;
using ProyectoFinalServicioCliente.UI.rArticulo;
using ProyectoFinalServicioCliente.UI.rCategoria;
using ProyectoFinalServicioCliente.UI.rCompras;
using ProyectoFinalServicioCliente.UI.rUsuarios;
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
    }
}
