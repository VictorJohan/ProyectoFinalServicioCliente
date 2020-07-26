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
    public partial class rFotografo : Window
    {
        Fotografos Fotografo = new Fotografos();
        public rFotografo()
        {
            InitializeComponent();
            this.DataContext = Fotografo;
            SexoComboBox.Items.Add("Femenino");
            SexoComboBox.Items.Add("Masculino");
        }
        private void Limpiar()
        {
            Fotografo = new Fotografos();
            this.DataContext = Fotografo;
        }
        
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}