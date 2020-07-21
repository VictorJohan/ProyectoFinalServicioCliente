using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using ProyectoFinalServicioCliente.UI.rArticulo;
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
        private Articulos articulo;
        private double precio, total;
        private int cantidad;
        public rCompras()
        {
            InitializeComponent();
            this.DataContext = Compra;
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            ArticuloIdComboBox.SelectedValuePath = "Articulo";
            ArticuloIdComboBox.DisplayMemberPath = "ArticuloId";

            CategoriaDetalleComboBox.ItemsSource = CategoriasBLL.GetListCategorias();
            CategoriaDetalleComboBox.SelectedValuePath = "CategoriaId";
            CategoriaDetalleComboBox.DisplayMemberPath = "Nombre";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var encontrado = ComprasBLL.Buscar(int.Parse(CompraIdTextBox.Text));

            if (encontrado != null)
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
            //todo: Programar el cambio de categoria, precio de venta y precio de compra en caso de que lo haya.
            //todo: Programar el incremento del Stock
            double costo = double.Parse(PrecioDetalleTextBox.Text) * int.Parse(CantidadDetalleTextBox.Text);

            var detalle = new ComprasDetalle
            {
                Articulo = articulo,
                Cantidad = int.Parse(CantidadDetalleTextBox.Text),
                Total = costo
            };

            Compra.Monto += costo;

            Compra.ComprasDetalles.Add(detalle);

            Cargar();

            LimpiarDetalle();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            //todo: Programar
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            //todo: Programar
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            //todo: Programar
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            //todo: Programar
        }

        private void ActualizarButton_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
            MessageBox.Show("Lista de articulos actualizada.", "Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void RegistrarArticuloButton_Click(object sender, RoutedEventArgs e)
        {
            rArticulos rArticulos = new rArticulos();
            rArticulos.Show();

        }


        public void Cargar()
        {
            this.DataContext = null;
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            this.DataContext = Compra;
        }

        private void PrecioDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            total = 0;

            if (!Regex.IsMatch(PrecioDetalleTextBox.Text, @"^[0-9]{1,3}$|^[0-9]{1,3}\.[0-9]{1,3}$"))

                precio = 0;
            else
                precio = double.Parse(PrecioDetalleTextBox.Text);

            total = cantidad * precio;
            TotalDetalleTextBox.Text = total.ToString();
        }

        private void CantidadDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[1-9]+$"))
                cantidad = 0;
            else
                cantidad = int.Parse(CantidadDetalleTextBox.Text);

            total = cantidad * precio;
            TotalDetalleTextBox.Text = total.ToString();
        }

        private void ArticuloIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArticuloIdComboBox.SelectedIndex == -1)
                return;
            articulo = (Articulos)ArticuloIdComboBox.SelectedItem;
            DescripcionDetalleTextBox.Text = articulo.Descripcion;
            StockDetalleTextBox.Text = articulo.Stock.ToString();
            CategoriaDetalleComboBox.SelectedValue = articulo.CategoriaId;
            PrecioVentaTextBox.Text = articulo.Precio.ToString();
            PrecioDetalleTextBox.Text = articulo.Costo.ToString();
        }

       

        public void LimpiarDetalle()
        {
            ArticuloIdComboBox.SelectedIndex = -1;
            DescripcionDetalleTextBox.Clear();
            StockDetalleTextBox.Clear();
            PrecioVentaTextBox.Clear();
            CategoriaDetalleComboBox.SelectedIndex = -1;
            PrecioDetalleTextBox.Clear();
            CantidadDetalleTextBox.Clear();
            TotalDetalleTextBox.Clear();
            ArticuloIdComboBox.Focus();
        }
    }
}
