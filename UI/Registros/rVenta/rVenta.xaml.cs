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
            ArticuloIdComboBox.DisplayMemberPath = "Descripcion";
            //Se llena el ComboBox Fotografos
            FotografoComboBox.ItemsSource = FotografosBLL.GetList();
            FotografoComboBox.SelectedValuePath = "FotografoId";
            FotografoComboBox.DisplayMemberPath = "Nombres";
        }

        //Busca un registro en la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo CompraId.
            {
                MessageBox.Show("El Id de la venta solo puede ser de carácter numérico.", "Campo Venta Id.",
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
            if (!válidarAgregar())
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
            if (!válidar())
                return;

            if (VentasBLL.Guardar(Venta))
            {

                Limpiar();
                MessageBox.Show("La venta fué registrada de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró guardar el registro de venta.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Elimina un registro de la base de datos.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Id de la venta solo puede ser de carácter numérico.", "Campo VentaId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (VentasBLL.Eliminar(int.Parse(VentaIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Venta eliminada.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar la venta.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
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
            PrecioVentaTextBox.Clear();
            CantidadDetalleTextBox.Clear();
            SubtotalDetalleTextBox.Clear();
            ArticuloIdComboBox.Focus();
        }

        //válida antes de agregar.
        public bool válidarAgregar()
        {
            //válida que haya un valor válido en el campo CompraId.
            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("La Venta Id solo puede ser de carácter numérico.", "Campo Venta Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que no haya campos vacíos.
            if (PrecioVentaTextBox.Text.Length == 0 || CantidadDetalleTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegúrese de que no haya campos vacíos en el detalle antes de agregar.", "Campos vacíos.", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            //válida que haya una cantidad válida en el TextBox.
            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("Solo puede introducir carácteres numéricos.", "Cantidad de artículo no válido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Vaalida que haya suficientes articulos en el Stock
            if (articulo.Stock < int.Parse(CantidadDetalleTextBox.Text))
            {
                MessageBox.Show($"No hay suficiente {articulo.Descripcion} en el Stock({articulo.Stock}).", "Cantidad de artículo insuficiente.",
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

        //Este metodo válida los datos antes de guardar.
        public bool válidar()
        {
            //válida el campo CompraId
            if (!Regex.IsMatch(VentaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El venta Id solo puede ser de carácter numérico.", "Campo VentaId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que exista el cliente.
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
