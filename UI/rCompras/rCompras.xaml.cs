using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
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

namespace ProyectoFinalServicioCliente.UI.rCompras
{
    /// <summary>
    /// Interaction logic for rCompras.xaml
    /// </summary>
    public partial class rCompras : Window
    {
        private Compras Compra = new Compras();
        private double precio;
        private int cantidad;
        public rCompras()
        {
            InitializeComponent();
            this.DataContext = Compra;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = ComprasBLL.Buscar(int.Parse(CompraIdTextBox.Text));

            if(encontrado != null)
            {
                Compra = encontrado;
                this.DataContext = Compra;
            }
            else
            {
                MessageBox.Show("Puede ser que la compra no este registrada en la base de datos.", "No se encontro Compra.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            double costo = double.Parse(PrecioDetalleTextBox.Text) * int.Parse(CantidadArticulosDetalleTextBox.Text);

            var articulo = new Articulos
            {
                Descripcion = DescripcionDetalleTextBox.Text,
                Stock = int.Parse(StockDetalleTextBox.Text),
                Precio = double.Parse(PrecioDetalleTextBox.Text),
                
            };

            var detalle = new ComprasDetalle
            {
                Articulo = articulo,
                Cantidad = int.Parse(CantidadArticulosDetalleTextBox.Text),
                Costo = costo
            };

            Compra.Monto = +costo;

            Compra.ComprasDetalles.Add(detalle);

            Cargar();

            DescripcionDetalleTextBox.Clear();
            StockDetalleTextBox.Clear();
            PrecioDetalleTextBox.Clear();
            CantidadArticulosDetalleTextBox.Clear();
            CostoDetalleTextBox.Clear();
            DescripcionDetalleTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {

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
            this.DataContext = Compra;
        }

        private void PrecioDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //todo: Johan termina de programar estos metodos
        }

        private void CantidadArticulosDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CostoDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
