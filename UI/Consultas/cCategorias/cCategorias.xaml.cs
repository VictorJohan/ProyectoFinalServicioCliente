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
    /// Interaction logic for cCategorias.xaml
    /// </summary>
    public partial class cCategorias : Window
    {
        public cCategorias()
        {
            InitializeComponent();
            string[] filtro = { "Categoria Id", "Categoria" };
            FiltroComBox.ItemsSource = filtro;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var Listado = new List<Categorias>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0:
                        Listado = CategoriasBLL.GetList(c => c.CategoriaId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        try
                        {
                            
                            Listado = CategoriasBLL.GetList(c => c.Nombre == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                   
                }

            }
            else
            {
                Listado = CategoriasBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = Listado;
        }
    }
}
