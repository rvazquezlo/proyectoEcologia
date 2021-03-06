﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProyectoEcologia
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbUsuario.Focus();
        }

        /**
         * Cuando se presiona boton de Aceptar se comparan la contrasena y el usuario ingresados
         * con la informacion en la base de datos para dar o negar acceso a la ventana de Home
         */
        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conexion;
            SqlCommand comando;
            SqlDataReader lector;

            try
            {
                conexion = Conexion.agregarConexion();//Se conecta con sql
                comando = new SqlCommand(String.Format("select contrasena from Usuario where nombreUsuario = '{0}'",
                    tbUsuario.Text), conexion);//query solicitando contrasena correspondiente al nombre de usuario ingresado en el espacio correspondiente
                lector = comando.ExecuteReader();
                if (lector.Read() ) //hubo una contrasena para el usuario ingresado
                {
                    if (lector.GetString(0).Equals(pboxContrasena.Password.ToString()))//La contrasena que ingreso el usuario es correcta
                    {
                        Home ventana = new Home();
                        this.Close();//cierra esta ventana
                        ventana.Show();//manda a la siguiente ventana
                    }
                    else
                        MessageBox.Show("Contrasena equivocada. Por favor intente de nuevo.");
                }
                else
                    MessageBox.Show("Usuario equivocado. Por favor intente de nuevo");
                lector.Close();
                conexion.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("error: " + ex);
            }  
        }  
    }
}
