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
        //todo: Resolver el problema con la Foreign key y hacer las validaciones
        private Compras Compra = new Compras();
        private Articulos articulo;
        private double precio, total;
        private int cantidad;
        public rCompras()
        {
            InitializeComponent();
            this.DataContext = Compra;
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            ArticuloIdComboBox.SelectedValuePath = "ArticuloId";
            ArticuloIdComboBox.DisplayMemberPath = "ArticuloId";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo CompraId.
            {
                MessageBox.Show("La Compra Id solo puede ser de caracter numerico.", "Campo Compra Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = ComprasBLL.Buscar(int.Parse(CompraIdTextBox.Text));

            if (encontrado != null)
            {
                Compra = encontrado;
                this.DataContext = Compra;
            }
            else
            {
                MessageBox.Show("Puede ser que la compra no este registrada en la base de datos.",
                    "No se encontro Compra.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;

            double costo = double.Parse(PrecioDetalleTextBox.Text) * int.Parse(CantidadDetalleTextBox.Text);
            Articulos Aux;
            Aux = ArticulosBLL.Buscar(int.Parse(ArticuloIdComboBox.SelectedValue.ToString()));
            var detalle = new ComprasDetalle
            {
                CompraId = int.Parse(CompraIdTextBox.Text),
                Cantidad = int.Parse(CantidadDetalleTextBox.Text),
                Total = total,
                ArticuloId = int.Parse(ArticuloIdComboBox.SelectedValue.ToString()),
                Articulo = articulo
            };

            Compra.ComprasDetalles.Add(detalle);

            Aux.Stock += int.Parse(CantidadDetalleTextBox.Text);
            Aux.Costo = double.Parse(PrecioDetalleTextBox.Text);
            Aux.Precio = double.Parse(PrecioVentaTextBox.Text);
            Compra.Monto += costo;

            ArticulosBLL.Guardar(Aux);

            Cargar();

            LimpiarDetalle();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            Articulos articulos = new Articulos();
            var aux = (ComprasDetalle)DetalleDataGrid.SelectedItem;
            articulos = ArticulosBLL.Buscar(aux.ArticuloId);
            articulos.Stock -= aux.Cantidad;

            Compra.Monto -= aux.Total;

            ArticulosBLL.Guardar(articulos);

            Compra.ComprasDetalles.RemoveAt(DetalleDataGrid.SelectedIndex);

            Cargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (ComprasBLL.Guardar(Compra))
            {

                Limpiar();
                MessageBox.Show("La Compra fue registrada de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro guardar el registro de compra.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de caracter numerico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ComprasBLL.Eliminar(int.Parse(CompraIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Compra eliminada.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar la compra.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            articulo.ArticuloId = 0;
            DescripcionDetalleTextBox.Text = articulo.Descripcion;
            StockDetalleTextBox.Text = articulo.Stock.ToString();
            CategoriaDetalleTextBox.Text = articulo.CategoriaId.ToString();
            PrecioVentaTextBox.Text = articulo.Precio.ToString();
            PrecioDetalleTextBox.Text = articulo.Costo.ToString();
        }

        public void LimpiarDetalle()
        {
            ArticuloIdComboBox.SelectedIndex = -1;
            DescripcionDetalleTextBox.Clear();
            StockDetalleTextBox.Clear();
            CategoriaDetalleTextBox.Clear();
            PrecioVentaTextBox.Clear();
            PrecioDetalleTextBox.Clear();
            CantidadDetalleTextBox.Clear();
            TotalDetalleTextBox.Clear();
            ArticuloIdComboBox.Focus();
        }

        public void Limpiar()
        {
            Compra = new Compras();
            this.DataContext = Compra;
        }

        public bool ValidarAgregar()
        {
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo CompraId.
            {
                MessageBox.Show("La Compra Id solo puede ser de caracter numerico.", "Campo Compra Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if(DescripcionDetalleTextBox.Text.Length == 0 || StockDetalleTextBox.Text.Length == 0 ||
                CategoriaDetalleTextBox.Text.Length == 0 || PrecioVentaTextBox.Text.Length == 0 ||
                PrecioDetalleTextBox.Text.Length == 0 || CantidadDetalleTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurese de que no haya campos vacios en el detalle antes de agregar.", "Campos vacios.", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            if (!Regex.IsMatch(PrecioDetalleTextBox.Text, @"^[0-9]{1,3}$|^[0-9]{1,3}\.[0-9]{1,3}$"))
            {
                MessageBox.Show("Solo puede introducir caracteres numericos.", "Costo de articulo no valido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Solo puede introducir caracteres numericos.", "Cantidad de articulo no valido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        public bool Validar()
        {
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de caracter numerico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
