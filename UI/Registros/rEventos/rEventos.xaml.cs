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

namespace ProyectoFinalServicioCliente.UI.rEventos
{
    /// <summary>
    /// Interaction logic for rEventos.xaml
    /// </summary>
    public partial class rEventos : Window
    {
        private Eventos Evento = new Eventos();
       
        
        public rEventos()
        {
            InitializeComponent();
            this.DataContext = Evento;
            FotografoComboBox.ItemsSource = FotografosBLL.GetList();
            FotografoComboBox.SelectedValuePath = "FotografoId";
            FotografoComboBox.DisplayMemberPath = "Nombres";
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            //válida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("Este cliente no esta registrado.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo ClienteId.
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var encontrado = EventosBLL.Buscar(int.Parse(ClienteIdTextBox.Text));
            
            if (encontrado != null)
            {
                Evento = encontrado;
                this.DataContext = Evento;
            }
            else
            {
                MessageBox.Show("Este cliente no tine eventos.", "Sin eventos.", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }

        private void AgregarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!válidar())
                return;

            var detalle = new EventosDetalle {
                ClienteId = int.Parse(ClienteIdTextBox.Text),
                Descripcion = DescripcionTextBox.Text,
                Lugar = LugarTextBox.Text,
                FotografoId = int.Parse(FotografoComboBox.SelectedValue.ToString()),
                Fecha = (DateTime)IniciaDatePicker.SelectedDate,
                FechaVencimiento = (DateTime)VenceDatePicker.SelectedDate,
                Precio = double.Parse(PrecioTextBox.Text)
            };
            
            Evento.EventosDetalles.Add(detalle);
            Evento.Total += double.Parse(PrecioTextBox.Text);

            Cargar();
            DescripcionTextBox.Clear();
            LugarTextBox.Clear();
            IniciaDatePicker.SelectedDate = null;
            VenceDatePicker.SelectedDate = null;
            PrecioTextBox.Clear();
            DescripcionTextBox.Focus();
        }

        private void RemoverButton_Click(object sender, RoutedEventArgs e)
        {
            var detalle = (EventosDetalle)DetalleDataGrid.SelectedItem;

            Evento.Total -= detalle.Precio;

            Evento.EventosDetalles.RemoveAt(DetalleDataGrid.SelectedIndex);
            Cargar();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }


        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo ClienteId.
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //válida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("El cliente debe estar registrado para poder agendar un evento.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EventosBLL.Guardar(Evento))
            {
                Limpiar();
                MessageBox.Show("Los eventos fuéron registrado de forma Éxitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se lograron registrar los eventos.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))//válida que haya un valor válido en el campo ArticuloId.
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EventosBLL.Eliminar(int.Parse(ClienteIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Se han eliminado todos los eventos de este clinete.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salió mal, no se lograron eliminar los eventos.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Evento;
        }

        public void Limpiar()
        {
            Evento = new Eventos();
            this.DataContext = Evento;
        }

        public bool válidar()
        {
            //válida que haya un valor válido en el campo ClienteId.
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Cliente Id solo puede ser de carácter numérico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("El cliente debe estar registrado para poder agendar un evento.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //válida que no haya campos vacíos.
            if (DescripcionTextBox.Text.Length == 0 || LugarTextBox.Text.Length == 0 ||
                PrecioTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegúrese de que no haya campos vacíos en el detalle antes de agregar.", "Campos vacíos.", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            //válida que se haya selecionado las fechas.
            if (IniciaDatePicker.SelectedDate == null || VenceDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Asegúrese de haber selcionado la fecha de inicio y fin del evento antes de agregar.", 
                    "Campos vacíos.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            //válida que la fecha de vencimineto sea mayor que la fecha de inicio
            DateTime inicio = IniciaDatePicker.SelectedDate.Value;
            DateTime vence = VenceDatePicker.SelectedDate.Value;
            if(IniciaDatePicker.SelectedDate.Value > VenceDatePicker.SelectedDate.Value)
            {
                MessageBox.Show("Asegúrese de haber selcionado correctamente las fechas.",
                    "La fecha de vencimiento es menor que la de inicio.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            //Válida que la fecha de inicio no sea menor que la fecha actual
            if(inicio < DateTime.Now)
            {
                MessageBox.Show("Asegúrese de haber selcionado correctamente las fechas.",
                    "La fecha de inicio ya pasó.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            //válida que se haya selccionado un fotografo
            if (FotografoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione un fotógrafo para poder agregar el evento.",
                    "No ha selecionado fotógrafo.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            //válida que se haya introducido un precio válido
            if (!Regex.IsMatch(PrecioTextBox.Text, @"^[0-9]{1,8}$|^[0-9]{1,8}\.[0-9]{1,8}$"))
            {
                MessageBox.Show("En el campo precio solo pueden haber carácteres numéricos.", "Campo Precio.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}
