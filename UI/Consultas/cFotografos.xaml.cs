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

namespace ProyectoFinalServicioCliente.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cFotografos.xaml
    /// </summary>
    public partial class cFotografos : Window
    {
        public cFotografos()
        {
            InitializeComponent();
            string[] filtro = { "Id", "Nombres","Apellido", "Cédula", "Dirección", "Teléfono", "Celular",
                "E-Mail","Sexo", "Fecha de Nacimiento" };
            FiltroComBox.ItemsSource = filtro;
        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Fotografos>();

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
                        listado = FotografosBLL.GetList(c => c.FotografoId == int.Parse(CriterioTexBox.Text));
                        break;
                    case 1:
                        try
                        {
                            listado = FotografosBLL.GetList(c => c.Nombres == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 2:
                        try
                        {

                            listado = FotografosBLL.GetList(c => c.Nombres.Contains(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 3:
                        try
                        {
                            if (!Regex.IsMatch(CriterioTexBox.Text, @"\d{3}-\d{7}-\d{1}"))
                            {
                                MessageBox.Show("Asegúrese de cumplir con el siguiente formato: 111-1111111-1.", "Verifique que haya ingresado una cédula válida",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }

                            listado = FotografosBLL.GetList(c => c.Cedula == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 4:
                        try
                        {

                            listado = FotografosBLL.GetList(c => c.Direccion == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 5:
                        try
                        {
                            //válida que se le haya colocado el prefijo al celular no (ejemplo: +1).
                            if (!Regex.IsMatch(CriterioTexBox.Text, @"^(\+[0-9]{1,12})$"))
                            {
                                MessageBox.Show("Asegúrese de haber colocado el prefijo telefonico correspondiente.", "Número celular no válido.",
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }

                            //válidando la longitud del telefono.
                            if (CriterioTexBox.Text.Length != 0 && CriterioTexBox.Text.Length < 8)
                            {
                                MessageBox.Show("El número de teléfono no cumple con una longitud válida.", "Longitud no válida.",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            listado = FotografosBLL.GetList(c => c.Telefono == CriterioTexBox.Text);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 6:
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


                            listado = FotografosBLL.GetList(C => C.Celular == CriterioTexBox.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 7:
                        try
                        {

                            //válida la dirreccion de correo electrónico.
                            if (!Regex.IsMatch(CriterioTexBox.Text, "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"))
                            {
                                MessageBox.Show("La direccón de correo electrónico que ha introducido no es válida.", "Campo Email.",
                                   MessageBoxButton.OK, MessageBoxImage.Information);
                                return;
                            }
                            listado = FotografosBLL.GetList(C => C.Email == CriterioTexBox.Text);

                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 8:
                        try
                        {
                            listado = FotografosBLL.GetList(C => C.Sexo == CriterioTexBox.Text);
                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                    case 9:
                        try
                        {
                            listado = FotografosBLL.GetList(C => C.FechaNacimiento == FechaNacimientoDesdeDatePicker.SelectedDate.Value);
                        }
                        catch
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                    case 10:
                        try
                        {
                            if (FechaNacimientoDesdeDatePicker.SelectedDate != null)
                                listado = FotografosBLL.GetList(c => c.FechaNacimiento.Date >= FechaNacimientoDesdeDatePicker.SelectedDate);

                            if (FechaNacimientoHastaDatePicker.SelectedDate != null)
                                listado = FotografosBLL.GetList(c => c.FechaNacimiento.Date <= FechaNacimientoHastaDatePicker.SelectedDate);

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
                listado = FotografosBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = listado;
        }
    }
}