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
    /// Interaction logic for cUsuarios.xaml
    /// </summary>
    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
            string[] filtro = { "UsuarioId", "Nombre", "Apellido","Usuario", "Fecha" };
            FiltroComBox.ItemsSource = filtro;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuarios>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0:
                        listado = UsuariosBLL.GetList(c => c.UsuarioId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        try
                        {

                            listado = UsuariosBLL.GetList(c => c.Nombres == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {

                            listado = UsuariosBLL.GetList(c => c.Apellidos == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 3:
                        try
                        {

                            listado = UsuariosBLL.GetList(c => c.Usuario ==CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 4:
                        try
                        {
                            listado = UsuariosBLL.GetList(C => C.Fecha == FechaCreacionDesdeDatePicker.SelectedDate.Value);
                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 5:
                        try
                        {
                            if (FechaCreacionDesdeDatePicker.SelectedDate != null)
                                listado = UsuariosBLL.GetList(c => c.Fecha.Date >= FechaCreacionDesdeDatePicker.SelectedDate);

                            if (FechaCreacionHastaDatePicker.SelectedDate != null)
                                listado = UsuariosBLL.GetList(c => c.Fecha.Date <= FechaCreacionHastaDatePicker.SelectedDate);

                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                }
            }
            else
            {
                listado = UsuariosBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = listado;
        }
    }
}