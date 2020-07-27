using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using ProyectoFinalServicioFotografo.BLL;
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

namespace ProyectoFinalServicioCliente.UI.rVenta
{
    /// <summary>
    /// Interaction logic for rVenta.xaml
    /// </summary>
    public partial class rVenta : Window
    {
        private Ventas Venta = new Ventas();
        private Articulos articulo;
        public rVenta()
        {
            InitializeComponent();
            this.DataContext = Venta;
            //Se llena el ComboBox ArticulosId
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            ArticuloIdComboBox.SelectedValuePath = "ArticuloId";
            ArticuloIdComboBox.DisplayMemberPath = "ArticuloId";
            //Se llena el ComboBox Fotografos
            FotografoComboBox.ItemsSource = FotografosBLL.GetList();
            FotografoComboBox.SelectedValuePath = "FotografoId";
            FotografoComboBox.DisplayMemberPath = "Nombres";
        }

        //Busca un registro en la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo CompraId.
            {
                MessageBox.Show("El Id de la venta solo puede ser de caracter numerico.", "Campo VentaId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = VentasBLL.Buscar(int.Parse(VentaIdTextBox.Text));

            if (encontrado != null)
            {
                Venta = encontrado;
                this.DataContext = Venta;
            }
            else
            {
                MessageBox.Show("Puede ser que la venta no este registrada en la base de datos.",
                    "No se encontro venta.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Agrega un articculo al detalle.
        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidarAgregar())
                return;

            Articulos Aux;//Este auxiliar hara los cambios en la base de datos;
            Aux = ArticulosBLL.Buscar(int.Parse(ArticuloIdComboBox.SelectedValue.ToString()));
            var detalle = new VentasDetalle
            {
                VentaId = int.Parse(VentaIdTextBox.Text),
                Cantidad = int.Parse(CantidadDetalleTextBox.Text),
                Subtotal = double.Parse(SubtotalDetalleTextBox.Text),
                ArticuloId = int.Parse(ArticuloIdComboBox.SelectedValue.ToString()),
                Articulo = articulo
            };

            Venta.VentasDetalles.Add(detalle);

            Aux.Stock -= int.Parse(CantidadDetalleTextBox.Text);
            Venta.Total += double.Parse(SubtotalDetalleTextBox.Text);

            ArticulosBLL.Guardar(Aux);

            Cargar();
            LimpiarDetalle();
        }

        //Quita una columna del detalle.
        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            Articulos articulos = new Articulos();
            var aux = (VentasDetalle)DetalleDataGrid.SelectedItem;
            articulos = ArticulosBLL.Buscar(aux.ArticuloId);
            articulos.Stock += aux.Cantidad;

            ArticulosBLL.Guardar(articulos);

            Venta.VentasDetalles.RemoveAt(DetalleDataGrid.SelectedIndex);
            Cargar();
        }

        //Limpia el registro para crear uno nuevo.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            LimpiarDetalle();
        }

        //Guarda un registro en la base de datos .
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (VentasBLL.Guardar(Venta))
            {

                Limpiar();
                MessageBox.Show("La venta fue registrada de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro guardar el registro de venta.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Elimina un registro de la base de datos.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Id de la venta solo puede ser de caracter numerico.", "Campo VentaId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (VentasBLL.Eliminar(int.Parse(VentaIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Venta eliminada.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar la venta.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Ayuda calcula el subtotal.
        private void CantidadDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cantidad;
            double precio, subtotal;

            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[0-9]+$"))
            {
                cantidad = 0;
                subtotal = 0;
            }
            else
            {
                cantidad = int.Parse(CantidadDetalleTextBox.Text);
                precio = double.Parse(PrecioVentaTextBox.Text);
                subtotal = cantidad * precio;
            }

            SubtotalDetalleTextBox.Text = subtotal.ToString();
        }

        //Este SelectionChanged es para selecionar el articulo que llenara los TextBox del detalle.
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

        }

        //Refresca el DataContex.
        public void Cargar()
        {
            this.DataContext = null;
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            this.DataContext = Venta;
        }

        //Limpia el detalle.
        public void LimpiarDetalle()
        {
            ArticuloIdComboBox.SelectedIndex = -1;
            DescripcionDetalleTextBox.Clear();
            StockDetalleTextBox.Clear();
            CategoriaDetalleTextBox.Clear();
            PrecioVentaTextBox.Clear();
            CantidadDetalleTextBox.Clear();
            SubtotalDetalleTextBox.Clear();
            ArticuloIdComboBox.Focus();
        }

        //Valida antes de agregar.
        public bool ValidarAgregar()
        {
            //Valida que haya un valor valido en el campo CompraId.
            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("La Venta Id solo puede ser de caracter numerico.", "Campo Venta Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que no haya campos vacios.
            if (DescripcionDetalleTextBox.Text.Length == 0 || StockDetalleTextBox.Text.Length == 0 ||
                CategoriaDetalleTextBox.Text.Length == 0 || PrecioVentaTextBox.Text.Length == 0 || CantidadDetalleTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurese de que no haya campos vacios en el detalle antes de agregar.", "Campos vacios.", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            //Valida que haya una cantidad valida en el TextBox.
            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Solo puede introducir caracteres numericos.", "Cantidad de articulo no valido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Vaalida que haya suficientes articulos en el Stock
            if (int.Parse(StockDetalleTextBox.Text) < int.Parse(CantidadDetalleTextBox.Text))
            {
                MessageBox.Show($"No hay suficiente {DescripcionDetalleTextBox.Text} en el Stock.", "Cantidad de articulo no valido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        //Limpia.
        public void Limpiar()
        {
            Venta = new Ventas();
            this.DataContext = Venta;
        }

        //Este metodo valida los datos antes de guardar.
        public bool Validar()
        {
            //Valida el campo CompraId
            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El venta Id solo puede ser de caracter numerico.", "Campo VentaId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("El cliente debe estar registrado para poder registrar la venta.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}
