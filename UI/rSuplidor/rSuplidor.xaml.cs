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

namespace ProyectoFinalServicioCliente.UI.rSuplidor
{
    /// <summary>
    /// Interaction logic for rSuplidor.xaml
    /// </summary>
    public partial class rSuplidor : Window
    {
        private Suplidores Suplidor = new Suplidores();
        public rSuplidor()
        {
            InitializeComponent();
            this.DataContext = Suplidor;
        }
        
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = SuplidoresBLL.Buscar(int.Parse(SuplidorIdTextBox.Text));

            if(encontrado != null)
            {
                Suplidor = encontrado;
                this.DataContext = Suplidor;
            }
            else
            {
                MessageBox.Show("Puede ser que el suplidor no este registrado en la base de datos.", "No se encontro el Suplidor.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (SuplidoresBLL.Guardar(Suplidor))
            {
                Limpiar();
                MessageBox.Show("El Suplidor fue registrado de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro registrar el Suplidor.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (SuplidoresBLL.Eliminar(int.Parse(SuplidorIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Suplidor eliminado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }                                     
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar el Suplidor.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }                                       
        }

        public void Limpiar()
        {   
            Suplidor = new Suplidores();
            this.DataContext = Suplidor;
        }

        public bool Validar()
        {
            //todo: Programar el metodo validar


            return true;
        }
    }
}
