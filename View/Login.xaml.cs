using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SistemaGestion.View
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["miConexion"].ConnectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUser.Text.Trim()) || string.IsNullOrEmpty(txtPassword.Password.Trim()))
            {
                MessageBox.Show("Ingrese todo lo campos Usuario y Contraseña", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            else
            {
               
                if (ValidarCredenciales(txtUser.Text.Trim(), txtPassword.Password.Trim()))
                {
                    MessageBox.Show("Inicio sección correcto", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    new MainWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }

            
           


        }

        private bool ValidarCredenciales(string usuario , string password )
        {
            

            string query = "SELECT COUNT(*) FROM Users WHERE Username=@usuario AND PasswordHash=@password ";

            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using(SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario",usuario);
                        cmd.Parameters.AddWithValue("@password",password);
                        return (int)cmd.ExecuteScalar() > 0;
                    }

                }
                catch(SqlException ex)
                {
                    MessageBox.Show($"Error SQL al consultar: {ex}", "advertencia",MessageBoxButton.OK,MessageBoxImage.Warning);
                    return false;
                }
            }

        }

        


    }
}
