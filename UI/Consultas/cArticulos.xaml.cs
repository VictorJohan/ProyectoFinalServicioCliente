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

namespace ProyectoFinalServicioCliente.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cArticulos.xaml
    /// </summary>
    public partial class cArticulos : Window
    {
        public cArticulos()
        {
            InitializeComponent();
            string[] filtro = { "Id", "Descripcion","Precio", "Ganacia","Stock","Costo" };
            FiltroComBox.ItemsSource = filtro;
        }
      

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var Listado = new List<Articulos>();

            if (CriterioTexBox.Text.Length != 0)//si no hay nada en el criterio hace una busqueda general(muestra todo)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0: 
                        Listado = ArticulosBLL.GetList(a => a.ArticuloId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        try
                        {
                            int id = Convert.ToInt32(CriterioTexBox.Text);
                            Listado = ArticulosBLL.GetList(c => c.ArticuloId == id);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {
                            int id = Convert.ToInt32(CriterioTexBox.Text);
                            Listado = ArticulosBLL.GetList(c => c.UsuarioId == id);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 3:
                        try
                        {

                            Listado = ArticulosBLL.GetList(c => c.Descripcion.Contains(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 4:
                        int a = Convert.ToInt32(CriterioTexBox.Text);
                        Listado = ArticulosBLL.GetList(s => s.CategoriaId == a);
                        break;
                    case 5:
                        try
                        {
                            int stock = Convert.ToInt32(CriterioTexBox.Text);
                            Listado = ArticulosBLL.GetList(c => c.Stock == stock);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese una cantidad valida");
                        }
                        break;

                    case 6:
                        try
                        {
                            int precio = Convert.ToInt32(CriterioTexBox.Text);
                            Listado = ArticulosBLL.GetList(c => c.Precio == precio);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese una cantidad valida");
                        }
                        break;

                    case 7:
                        try
                        {
                            double costo = Convert.ToInt32(CriterioTexBox.Text);
                           Listado = ArticulosBLL.GetList(c => c.Costo == costo);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese una cantidad valida");
                        }
                        break;
                }

            }
            else
            {
                Listado = ArticulosBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = Listado;
        }
    }
}