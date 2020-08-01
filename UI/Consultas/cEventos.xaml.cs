﻿using ProyectoFinalServicioCliente.BLL;
using ProyectoFinalServicioCliente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for cEventos.xaml
    /// </summary>
    public partial class cEventos : Window
    {
        public cEventos()
        {
            InitializeComponent();
            string[] filtro = { "Id", "Descipcion", "Lugar" };
            FiltroComBox.ItemsSource = filtro;
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Buscar_Click(object sender, RoutedEventArgs e)
        {
            var Listado = new List<Eventos>();

            if (CriterioTexBox.Text.Trim().Length > 0)
            {
                switch (FiltroComBox.SelectedIndex)
                {
                    case 0://Todo
                        Listado = EventosBLL.GetList(s => true);
                        break;
                    case 1:
                        try
                        {
                            int id = Convert.ToInt32(CriterioTexBox.Text);
                          //  Listado = EventosBLL.GetList(c => c.EventoId == id);
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
                            Listado = EventosBLL.GetList(c => c.UsuarioId == id);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un ID valido");
                        }
                        break;
                    case 3:
                        try
                        {

                         //  Listado = EventosBLL.GetList(c => c.Descripcion.Contains(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                    case 4:
                        try
                        {

                          //  Listado = EventosBLL.GetList(c => c.Lugar.Contains(CriterioTexBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;

                    case 5:
                        try
                        {
                            decimal precio = Convert.ToDecimal(CriterioTexBox.Text);
                           // Listado = EventosBLL.GetList(c => c.Precio == precio);
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;



                }

                if (DesdeDataPicker.SelectedDate != null && HastaDatePicker.SelectedDate != null) ;
                  //  Listado = Listado.Where(c => c.FechaInicio.Date >= DesdeDataPicker.SelectedDate.Value && c.FechaFin.Date <= HastaDatePicker.SelectedDate.Value).ToList();

            }
            else
            {
                Listado = EventosBLL.GetList(c => true);
            }

            ConsultaDataGrid.ItemsSource = null;
            ConsultaDataGrid.ItemsSource = Listado;
        }

        private void ConsultaDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}