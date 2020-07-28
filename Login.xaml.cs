﻿using ProyectoFinalServicioCliente.BLL;
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


namespace ProyectoFinalServicioCliente
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        //Este metodo cierra el programa en caso de cerrar la ventana de Login
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }


        private void IngresarButton_Click1(object sender, RoutedEventArgs e)
        {
            bool paso = UsuariosBLL.Autenticar(NombreUsuarioTextBox.Text, ContrasenaPasswordBox.Password);

            if (paso)
            {
                this.Close();
                MessageBox.Show("Entraste");
                //Hay que hacer la ventana principal
                //Principal.Show();
            }
            else
            {
                MessageBox.Show("El Usuario o Contraseña incorrecta.", "Las credenciales no son correctas.", MessageBoxButton.OK, MessageBoxImage.Error);
                ContrasenaPasswordBox.Clear();
                NombreUsuarioTextBox.Focus();
            }
        }
    }
}

