using System;
using Caliburn.Micro;
using WarfaceToolsWPF.Models;

namespace WarfaceToolsWPF.ViewModels
{
    internal class WeaponStatsViewModel : PropertyChangedBase
    {
        private BindableCollection<Item> _bc;

        private void LoadWeapons(string source) { WeaponDataTable = DataTableManager.LoadDataTable(source, true); }

        public void ButtonRiflemanWeapons() { LoadWeapons(DataTableManager.RiflemanWeapons); }

        public void ButtonEngineerWeapons() { LoadWeapons(DataTableManager.EngineerWeapons); }

        public void ButtonMedicWeapons() { LoadWeapons(DataTableManager.MedicWeapons); }

        public void ButtonSniperWeapons() { LoadWeapons(DataTableManager.SniperWeapons); }

        public void ButtonPistols() { LoadWeapons(DataTableManager.Pistols); }

        public BindableCollection<Item> WeaponDataTable
        {
            get { return _bc; }
            set
            {
                _bc = value;
                Console.WriteLine(@"NotifyOfPropertyChange(()=>WeaponDataTable)");
                NotifyOfPropertyChange(() => WeaponDataTable);
            }
        }

    }
}