using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using WarfaceToolsWPF.Models;

namespace WarfaceToolsWPF.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class LoginViewModel : Screen
    {
        private readonly WindowManager _windowManager;
        private readonly User _user;
        private readonly Login _login;
        private string _windowTitle = "Login - WarfaceTools";

        public string WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                NotifyOfPropertyChange(()=> WindowTitle);
            }
        }

        public string Username
        {
            get { return _user.Username; }
            set
            {
                _user.Username = value;
                NotifyOfPropertyChange(()=>Username);
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                _user.Password = value;
                NotifyOfPropertyChange(()=>Password);
            }
        }

        public LoginViewModel()
        {
            _windowManager = new WindowManager();
            _user = User.Instance;
            _login = Login.Instance;
        }

        public void ButtonLogin()
        {
            Console.WriteLine(@"Login button clicked");
            {
                Login.DatabaseConnect();
                try
                {
                    Login.Authenticate(_user);
                }
                catch (CustomException e)
                {
                    if (e.ErrorCode == 1000)
                        MessageBox.Show("Cannot connect to server. Contact administrator.", "Error");
                    else if (e.ErrorCode == 1001)
                        MessageBox.Show("Enter username.", "Error");
                    else if (e.ErrorCode == 1002)
                        MessageBox.Show("Enter password.", "Error");
                    else if (e.ErrorCode == 1003)
                        MessageBox.Show("User with this username and password doesn't exist.", "Error");
                }
                if (!_user.IsLoggedIn) return;
                _windowManager.ShowWindow(new MainAppViewModel());
                TryClose();
            }  
        }

        /// <summary>
        /// Jeżeli został wciśnięty klawisz enter aktywuje ButtonLogin()
        /// </summary>
        /// <param name="context"></param>
        public void KeyPressed(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;
            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                ButtonLogin();
            }
        }

        public void ButtonRegister()
        {
            Console.Write(@"Register button clicked");
            Process.Start(
                "http://www.google.com/?q=Portal%20do%20zakładania%20konta%20nie%20został%20zaimplementowany%20jako%20że%20nie%20jest%20częścią%20projektu.");
        }
    }
}
