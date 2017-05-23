using System;
using System.Windows;
using Caliburn.Micro;
using WarfaceToolsWPF.Models;

namespace WarfaceToolsWPF.ViewModels
{
    /// <summary>
    /// ViewModel dający możliwość zmiany ViewModelu w tym samym oknie
    /// Poszczególne metody poprzez ActivateItem(...) zmieniają ViewModel
    /// </summary>
    internal class MainAppViewModel : Conductor<object>
    {
        private readonly WindowManager _windowManager;
        private readonly User _user = User.Instance;
        private readonly DataTableManager _dtm = new DataTableManager();

        public MainAppViewModel()
        {
            _windowManager = new WindowManager();
            if (_dtm.CheckIfDataTablesExist()) return;
            MessageBox.Show("Database files were not found. They will be downloaded.", "Files not found");
            _dtm.ReDownloadDataTables();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            ActivateItem(new MainScreenViewModel());
            DisplayName = "WarfaceTools";
        }

        public void DisplayMainScreenViewModel()
        {
            ActivateItem(new MainScreenViewModel());
            Console.WriteLine(@"MainScreen enabled");
        }

        public void DisplayWeaponStatsViewModel()
        {
            ActivateItem(new WeaponStatsViewModel());
            Console.WriteLine(@"Weapon enabled");
        }
        public void DisplayEquipmentStatsViewModel()
        {
            ActivateItem(new EquipmentStatsViewModel());
            Console.WriteLine(@"Equipment enabled");
        }
        public void DisplayComparisonViewModel()
        {
            ActivateItem(new ComparisonViewModel());
            Console.WriteLine(@"Comparison enabled");
        }

        public void DisplayCalcViewModel()
        {
            ActivateItem(new CalcViewModel());
            Console.WriteLine(@"Calculator enabled");
        }

        public void DisplayCostCalcViewModel()
        {
            ActivateItem(new CostCalcViewModel());
            Console.WriteLine(@"Cost calculator enabled");
        }

        public void ButtonLogout()
        {
            Console.WriteLine(@"Logout button pressed.");
            if (MessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            _user.IsLoggedIn = false;
            Console.WriteLine(@"User logged out.");
            _windowManager.ShowWindow(new LoginViewModel());
            Login.Conn.Close();
            Console.WriteLine(@"Connection closed.");
            TryClose();
        }

        public void ButtonExit()
        {
            Console.WriteLine(@"Exit button pressed.");
            if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
            _user.IsLoggedIn = false;
            Console.WriteLine(@"User logged out.");
            Login.Conn.Close();
            Console.WriteLine(@"Connection closed.");
            TryClose();
        }
    }
}