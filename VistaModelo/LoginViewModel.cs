using System;
using System.Security;
using System.Windows.Input;
using System.Threading;
using System.Security.Principal;
using System.Windows;
using SistemaGestion.Repositories;
using System.Net;
using SistemaGestion.Models;

namespace SistemaGestion.VistaModelo
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true; // Inicializa en true para que sea visible inicialmente
        private readonly IUserRepository userRepository;

        // Propiedades
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public SecureString Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        // Comandos
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPassCommand("", ""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            // Simplificar la validación
            return !string.IsNullOrEmpty(Username) && Username.Length >= 3 &&
                   Password != null && Password.Length >= 3;
        }

        private void ExecuteLoginCommand(object obj)
        {
            try
            {
                var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
                if (isValidUser)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                    IsViewVisible = false;

                    // Mostrar ventana principal y cerrar ventana de login
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                        Application.Current.Windows[0].Close();
                    });
                }
                else
                {
                    ErrorMessage = "* Usuario o contraseña incorrecta";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al iniciar sesión: {ex.Message}";
            }
        }

        private void ExecuteRecoverPassCommand(string username, string email)
        {
            // Implementar lógica de recuperación de contraseña o dejar un mensaje.
            MessageBox.Show("Función de recuperación de contraseña no implementada.",
                            "Recuperar contraseña",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
        }

        private void ExecuteShowPasswordCommand(object obj)
        {
            // Lógica para mostrar u ocultar la contraseña si es necesario
        }

        private void ExecuteRememberPasswordCommand(object obj)
        {
            // Lógica para recordar credenciales si es necesario
        }
    }
}
