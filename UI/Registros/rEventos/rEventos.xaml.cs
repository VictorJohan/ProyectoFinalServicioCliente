﻿using ProyectoFinalServicioCliente.BLL;
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
            //Valida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("Este cliente no esta registrado.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo ClienteId.
            {
                MessageBox.Show("El Cliente Id solo puede ser de caracter numerico.", "Campo Clinete Id.",
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
            if (!Validar())
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
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo ClienteId.
            {
                MessageBox.Show("El Cliente Id solo puede ser de caracter numerico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //Valida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("El cliente debe estar registrado para poder agendar un evento.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EventosBLL.Guardar(Evento))
            {
                Limpiar();
                MessageBox.Show("Los eventos fueron registrado de forma exitosa.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se lograron registrar los eventos.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))//Valida que haya un valor valido en el campo ArticuloId.
            {
                MessageBox.Show("El Cliente Id solo puede ser de caracter numerico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (EventosBLL.Eliminar(int.Parse(ClienteIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Se han eliminado todos los eventos de este clinete.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Algo salio mal, no se lograron eliminar los eventos.", "Error.", MessageBoxButton.OK, MessageBoxImage.Error);
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

        public bool Validar()
        {
            //Valida que haya un valor valido en el campo ClienteId.
            if (!Regex.IsMatch(ClienteIdTextBox.Text, "^[1-9]+$"))
            {
                MessageBox.Show("El Cliente Id solo puede ser de caracter numerico.", "Campo Clinete Id.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que exista el cliente.
            if (!ClientesBLL.Existe(int.Parse(ClienteIdTextBox.Text)))
            {
                MessageBox.Show("El cliente debe estar registrado para poder agendar un evento.", "El Cliente no existe en la base de datos.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            //Valida que no haya campos vacios.
            if (DescripcionTextBox.Text.Length == 0 || LugarTextBox.Text.Length == 0 ||
                PrecioTextBox.Text.Length == 0)
            {
                MessageBox.Show("Asegurese de que no haya campos vacios en el detalle antes de agregar.", "Campos vacios.", MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return false;
            }

            //Valida que se haya selecionado las fechas.
            if (IniciaDatePicker.SelectedDate == null || VenceDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Asegurese de haber selcionado la fecha de inicio y fin del evento antes de agregar.", 
                    "Campos vacios.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            //Valida que la fecha de vencimineto sea mayor que la fecha de inicio
            DateTime inicio = IniciaDatePicker.SelectedDate.Value;
            DateTime vence = VenceDatePicker.SelectedDate.Value;
            if(IniciaDatePicker.SelectedDate.Value > VenceDatePicker.SelectedDate.Value)
            {
                MessageBox.Show("Asegurese de haber selcionado correctamente las fechas.",
                    "La fecha de vencimiento es menor que la de inicio.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }
            
            //Valida que se haya selccionado un fotografo
            if(FotografoComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione un fotografo para poder agregar el evento.",
                    "No ha selecionado fotografo.", MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            //Valida que se haya introducido un precio valido
            if (!Regex.IsMatch(PrecioTextBox.Text, @"^[0-9]{1,8}$|^[0-9]{1,8}\.[0-9]{1,8}$"))
            {
                MessageBox.Show("En el campo precio solo pueden haber caracteres numericos.", "Campo Precio.",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}