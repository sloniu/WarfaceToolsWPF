using Caliburn.Micro;
using WarfaceToolsWPF.Models;

namespace WarfaceToolsWPF.ViewModels
{
    internal class MainScreenViewModel : PropertyChangedBase
    {
        private static readonly User User = User.Instance;
        private string _welcomeMessage = "Witaj " + User.Username;

        public string WelcomeMessage
        {
            get { return _welcomeMessage; }
            set
            {
                _welcomeMessage = value;
                NotifyOfPropertyChange(()=> WelcomeMessage);
            }
        }
    }
}
