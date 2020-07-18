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

namespace ProyectoFinalServicioCliente.UI.rArticulo
{
    /// <summary>
    /// Interaction logic for rArticulos.xaml
    /// </summary>
    public partial class rArticulos : Window
    {
        private Articulos Articulo = new Articulos();
        private Categorias Categoria;
        public rArticulos()
        {
            InitializeComponent();
            this.DataContext = Articulo;
            CategoriaComboBox.ItemsSource = CategoriasBLL.GetListCategorias();
            CategoriaComboBox.SelectedValuePath = "Categorias";
            CategoriaComboBox.DisplayMemberPath = "Nombre";
        }

        //Boton que desencadenara el evento buscar para obtener un registro de la base de datos.
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ArticuloIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo ArticuloId.
            {
                MessageBox.Show("El Articulo Id solo puede ser de caracter numerico.", "Campo Articulo Id.",
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
            if (!ValidarGuardar())
                return;

            if (ArticulosBLL.Guardar(Articulo))
            {
                Limpiar();
                MessageBox.Show("El Articulo fue guardado de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro guardar el Articulo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {

            if (!Regex.IsMatch(ArticuloIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de caracter numerico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ArticulosBLL.Eliminar(int.Parse(ArticuloIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Articulo eliminado.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se logro eliminar el Articulo.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Limpiar()
        {
            Articulo = new Articulos();
            this.DataContext = Articulo;
            CategoriaComboBox.SelectedIndex = -1;
        }

        public bool ValidarGuardar()
        {
            //Valida que haya un valor valido en el campo ArticuloId.
            if (!Regex.IsMatch(ArticuloIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Articulo Id solo puede ser de caracter numerico.", "Campo Articulo Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que todos los campos esten llenos
            if (ArticuloIdTextBox.Text.Length == 0 || DescripcionTextBox.Text.Length == 0 || 
                StockTextBox.Text.Length == 0 || PrecioTextBox.Text.Length == 0 || CostoTextBox.Text.Length == 0)
            {
                MessageBox.Show("No pueden haber campos vacios.", "Campos vacios.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que se seleccione una categoria.
            if (CategoriaComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una categoria.", "Campos categoria.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que se ingrese una cantidad valida.
            if (!Regex.IsMatch(StockTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("En el campo Stock solo pueden haber caracteres numericos.", "Campo Stock.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que se ingrese un precio valido
            if (!Regex.IsMatch(PrecioTextBox.Text, @"(\d+(\.)?\d*)"))
            {
                MessageBox.Show("En el campo precio solo pueden haber caracteres numericos.", "Campo Precio.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que se ingrese un costo valido
            if (!Regex.IsMatch(CostoTextBox.Text, @"(\d+(\.)?\d*)"))
            {
                MessageBox.Show("En el campo costo solo pueden haber caracteres numericos.", "Campo Costo.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        //Almacena el Id de la categoria en la propiedad correspondiente
        private void CategoriaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriaComboBox.SelectedItem == null)
                return;

            Categoria = (Categorias)CategoriaComboBox.SelectedItem;
            Articulo.CategoriaId = Categoria.CategoriaId;
        }
    }
}
