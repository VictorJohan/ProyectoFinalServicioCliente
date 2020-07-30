using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ProyectoFinalServicioCliente.UI.rArticulo
{
    /// <summary>
    /// Interaction logic for rArticulos.xaml
    /// </summary>
    public partial class rArticulos : Window
    {
        private Articulos Articulo = new Articulos();
        double precio, ganancia, costo;
        public rArticulos()
        {
            InitializeComponent();
            this.DataContext = Articulo;
            CategoriaComboBox.ItemsSource = CategoriasBLL.GetListCategorias();
            CategoriaComboBox.SelectedValuePath = "CategoriaId";
            CategoriaComboBox.DisplayMemberPath = "Nombre";
            //CostoTextBox.Text = costo.ToString();
        }

        //Boton que desencadenara el evento buscar para obtener un registro de la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ArticuloIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo ArticuloId.
            {
                MessageBox.Show("El Articulo Id solo puede ser de carácter numérico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var encontrado = ArticulosBLL.Buscar(int.Parse(ArticuloIdTextBox.Text));

            if (encontrado != null)
            {
                Articulo = encontrado;
                this.DataContext = Articulo;
            }
            else
            {
                MessageBox.Show("Puede ser que el articulo no exista en el inventario.", "No se encontro el Articulo.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        //Boton que desencadenara el evento que limpiara el registro para crear uno nuevo.
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        //Boton que desencadenara el evento guardar en la base de datos.
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!validarGuardar())
                return;

            if (ArticulosBLL.Guardar(Articulo))
            {
                
                Limpiar();
                MessageBox.Show("El Articulo fué guardado de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró guardar el Articulo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!Regex.IsMatch(ArticuloIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de carácter numérico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ArticulosBLL.Eliminar(int.Parse(ArticuloIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Articulo eliminado.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar el Articulo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Limpiar()
        {
            Articulo = new Articulos();
            this.DataContext = Articulo;
            CategoriaComboBox.SelectedIndex = -1;
        }

       
        public bool validarGuardar()
        {
            //válida que haya un valor válido en el campo ArticuloId.
            if (!Regex.IsMatch(ArticuloIdTextBox.Text, "^[0-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de carácter numérico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que todos los campos esten llenos
            if (ArticuloIdTextBox.Text.Length == 0 || DescripcionTextBox.Text.Length == 0 || PrecioTextBox.Text.Length == 0)
            {
                MessageBox.Show("No pueden haber campos vacíos.", "Campos vacíos.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que se seleccione una categoria.
            if (CategoriaComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoria.", "Campos categoria.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if(CostoTextBox.Text == PrecioTextBox.Text || double.Parse(CostoTextBox.Text) > double.Parse(PrecioTextBox.Text))
            {
                MessageBox.Show("El precio no puede ser igual o menor que el costo.", "No esta obteniendo ninguna ganancia.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            ////válida que se ingrese una cantidad válida.
            //if (!Regex.IsMatch(StockTextBox.Text, "^[1-9]+$"))
            //{
            //    MessageBox.Show("En el campo Stock solo pueden haber carácteres numéricos.", "Campo Stock.",
            //        MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return false;
            //}

            //válida que se ingrese un precio válido
            if (!Regex.IsMatch(PrecioTextBox.Text, @"^[0-9]{1,8}$|^[0-9]{1,8}\.[0-9]{1,8}$"))
            {
                MessageBox.Show("En el campo precio solo pueden haber carácteres numéricos.", "Campo Precio.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            ////válida que se ingrese un costo válido
            //if (!Regex.IsMatch(CostoTextBox.Text, @"^[0-9]{1,3}$|^[0-9]{1,3}\.[0-9]{1,3}$"))
            //{
            //    MessageBox.Show("En el campo costo solo pueden haber carácteres numéricos.", "Campo Costo.",
            //        MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return false;
            //}

            return true;
        }

        private void PrecioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(PrecioTextBox.Text, @"^[0-9]{1,8}$|^[0-9]{1,8}\.[0-9]{1,8}$"))
            {
                precio = 0;
            }
            else
            {
                precio = double.Parse(PrecioTextBox.Text);
                costo = double.Parse(CostoTextBox.Text);

                ganancia = precio - costo;
                GananciasTextBox.Text = ganancia.ToString();
            }
        }

        private void GananciasTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            GananciasTextBox.Text = ganancia.ToString();
        }
    }
}
