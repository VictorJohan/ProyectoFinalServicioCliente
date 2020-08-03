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

namespace ProyectoFinalServicioCliente.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cSuplidor.xaml
    /// </summary>
    public partial class cSuplidor : Window
    {
        public cSuplidor()
        {
            InitializeComponent();
            string[] filtro = { "Id", "Nombres", "Teléfono", "E-Mail" };
            FiltroComBox.ItemsSource = filtro;
        }

        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Suplidores>();
            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0:
                        if (!Regex.IsMatch(CriterioTexBox.Text, "^[0-9]+$"))
                        {
                            MessageBox.Show("Se esperaba un Id no una cadena de texto", "Campo Criterio",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }

                        listado = SuplidoresBLL.GetList(c => c.SuplidorId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        try
                        {
                            listado = SuplidoresBLL.GetList(c => c.Nombres == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {

                            //válida que se le haya colocado el prefijo al celular no (ejemplo: +1).
                            if (!Regex.IsMatch(CriterioTexBox.Text, @"^(\+[0-9]{1,12})$"))
                            {
                                MessageBox.Show("Asegúrese de haber colocado el prefijo telefonico correspondiente.", "Número celular no válido.",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }

                            //válidando la longitud del celular.
                            if (CriterioTexBox.Text.Length < 8)
                            {
                                MessageBox.Show("El número celular no cumple con una longitud válida.", "Longitud no válida.",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }


                            listado = SuplidoresBLL.GetList(C => C.Telefono == CriterioTexBox.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 3:
                        try
                        {

                            //válida la dirreccion de correo electrónico.
                            if (!Regex.IsMatch(CriterioTexBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
                            {
                                MessageBox.Show("La direccón de correo electrónico que ha introducido no es válida.", "Campo Email.",
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            listado = SuplidoresBLL.GetList(C => C.Email == CriterioTexBox.Text);

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
                listado = SuplidoresBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = listado;
        }
    }
}
