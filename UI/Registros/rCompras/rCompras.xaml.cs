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
        private double costo, total;
        private int cantidad;
        public rCompras()
        {
            InitializeComponent();
            this.DataContext = Compra;
            //Se llena el ComboBox ArticulosId
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            ArticuloIdComboBox.SelectedValuePath = "ArticuloId";
            ArticuloIdComboBox.DisplayMemberPath = "Descripcion";
            //Se llena el ComboBox Suplidores
            SuplidorComboBox.ItemsSource = SuplidoresBLL.GetSuplidores();
            SuplidorComboBox.SelectedValuePath = "SuplidorId";
            SuplidorComboBox.DisplayMemberPath = "Nombres";
        }

        //Evento que buscara un registro.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo CompraId.
            {
                MessageBox.Show("El Id de la compra solo puede ser de carácter numérico.", "Campo CompraId.",
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

        //Evento que agregara el articulo al detalle.
        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidarAgregar())//todo: hacer los cambios en el codigo para que afecte la tabla. *Revisar el xamel y ver que todo este bien
                return;

            Articulos Aux;//Este auxiliar hara los cambios en la base de datos;
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
            Aux.Costo = double.Parse(CostoDetalleTextBox.Text);
            Compra.Monto += total;

            ArticulosBLL.Guardar(Aux);

            Cargar();

            LimpiarDetalle();
        }

        //Evento que removera una fila del detalle
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

        //Evento que limpiara el WPF para un nuevo registro.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //Evento que guardara un registro
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidar())
                return;

            if (ComprasBLL.Guardar(Compra))
            {

                Limpiar();
                MessageBox.Show("La Compra fué registrada de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró guardar el registro de compra.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Evento que eliminara un registro.
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Id de la compra solo puede ser de carácter numérico.", "Campo CompraId.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ComprasBLL.Eliminar(int.Parse(CompraIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Compra eliminada.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar la compra.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Eveto que actualizara el ComboBox en caso de que se agregue un articulo en el WPF de compra
        private void ActualizarButton_Click(object sender, RoutedEventArgs e)
        {
            Cargar();
            MessageBox.Show("Lista de articulos actualizada.", "Sistema", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Este evento llamara a rArticulos
        private void RegistrarArticuloButton_Click(object sender, RoutedEventArgs e)
        {
            rArticulos rArticulos = new rArticulos();
            rArticulos.Show();

        }

        //Este evento actualizara el detalle
        public void Cargar()
        {
            this.DataContext = null;
            ArticuloIdComboBox.ItemsSource = ArticulosBLL.GetListArticulos();
            this.DataContext = Compra;
        }

        //Calcula el total
        private void CantidadDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[1-9]+$"))
            {
                cantidad = 0;
            }
            else
            {
                cantidad = int.Parse(CantidadDetalleTextBox.Text);

                total = cantidad * costo;
                TotalDetalleTextBox.Text = total.ToString();

            }
        }
        //Calcula el total
        private void CostoDetalleTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(CostoDetalleTextBox.Text, @"^[0-9]{1,3}$|^[0-9]{1,3}\.[0-9]{1,3}$"))
            {
                costo = 0;
            }
            else
            {
                costo = double.Parse(CostoDetalleTextBox.Text);
                total = cantidad * costo;
                TotalDetalleTextBox.Text = total.ToString();
            }
        }

        //Este SelectionChanged es para selecionar el articulo que llenara los TextBox del detalle.
        private void ArticuloIdComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ArticuloIdComboBox.SelectedIndex == -1)
                return;

            articulo = (Articulos)ArticuloIdComboBox.SelectedItem;
            articulo.ArticuloId = 0;
            CostoDetalleTextBox.Text = articulo.Costo.ToString();
        }

        //Limpia los campos del detalle.
        public void LimpiarDetalle()
        {
            ArticuloIdComboBox.SelectedIndex = -1;
            CostoDetalleTextBox.Clear();
            CantidadDetalleTextBox.Clear();
            TotalDetalleTextBox.Clear();
            ArticuloIdComboBox.Focus();
        }

        //Limpia el WPF
        public void Limpiar()
        {
            Compra = new Compras();
            this.DataContext = Compra;
        }

        //Este metodo válidara los campos antes de agregar los datos.
        public bool válidarAgregar()
        {
            //válida que haya un valor válido en el campo CompraId.
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("La Compra Id solo puede ser de carácter numérico.", "Campo Compra Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que no haya campos vacíos.
            if (ArticuloIdComboBox.SelectedIndex == -1 || CostoDetalleTextBox.Text.Length == 0 ||
                CantidadDetalleTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegúrese de que no haya campos vacíos en el detalle antes de agregar.", "Campos vacíos.", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            //válida que haya un dato válido en el precio detalle.
            if (!Regex.IsMatch(CostoDetalleTextBox.Text, @"^[0-9]{1,3}$|^[0-9]{1,3}\.[0-9]{1,3}$"))
            {
                MessageBox.Show("Solo puede introducir carácteres numéricos.", "Costo de articulo no válido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que haya una cantidad válida en el TextBox.
            if (!Regex.IsMatch(CantidadDetalleTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("Solo puede introducir carácteres numéricos.", "Cantidad de articulo no válido.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        //Este metodo válida los datos antes de guardar.
        public bool válidar()
        {
            //válida el campo CompraId
            if (!Regex.IsMatch(CompraIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de carácter numérico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida el ComboBox
            if (SuplidorComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Debe selecionar un suplidor para registrar la compra.", "No hay un suplidor asignado a la compra.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }


    }
}
