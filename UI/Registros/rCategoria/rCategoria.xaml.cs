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

namespace ProyectoFinalServicioCliente.UI.rCategoria
{
    /// <summary>
    /// Interaction logic for rCategoria.xaml
    /// </summary>
    public partial class rCategoria : Window
    {
        private Categorias Categoria = new Categorias();
        
        public rCategoria()
        {
            InitializeComponent();
            this.DataContext = Categoria;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CategoriaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("La Categoria Id solo puede ser de carácter numérico.", "Campo Categoria Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = CategoriasBLL.Buscar(int.Parse(CategoriaIdTextBox.Text));

            if(encontrado != null)
            {
                Categoria = encontrado;
                this.DataContext = Categoria;
            }
            else
            {
                MessageBox.Show("Puede ser que la categoria no exista en la base de datos.", "No se encontro la Categoria.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidar())
            {
                return;
            }

            if (CategoriasBLL.Guardar(Categoria))
            {
                Limpiar();
                MessageBox.Show("Categoria guardada de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró guardar la categoria.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(CategoriaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("La Categoria Id solo puede ser de carácter numérico.", "Campo Categoria Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CategoriasBLL.Eliminar(int.Parse(CategoriaIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Categoria eliminada.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se logró eliminar la categoria.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Limpiar()
        {
            Categoria = new Categorias();
            this.DataContext = Categoria;
            
        }

        public bool válidar()
        {
            if(!Regex.IsMatch(CategoriaIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("La Categoria Id solo puede ser de carácter numérico.", "Campo Categoria Id.",
                     MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if(NombreTextBox.Text.Length == 0)
            {
                MessageBox.Show("El campo categoria no puede estar vacio.", "Escriba una categoria.", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}
