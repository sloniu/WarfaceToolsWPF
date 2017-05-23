using System;
using Caliburn.Micro;
using WarfaceToolsWPF.Models;

namespace WarfaceToolsWPF.ViewModels
{
    internal class EquipmentStatsViewModel : PropertyChangedBase
    {
        private BindableCollection<Item> _bc;

        private void LoadEquipment(string source) { EquipmentDataTable = DataTableManager.LoadDataTable(source, false); }

        public void ButtonRiflemanEquipment() { LoadEquipment(DataTableManager.RiflemanEquipment); }

        public void ButtonEngineerEquipment() { LoadEquipment(DataTableManager.EngineerEquipment); }

        public void ButtonMedicEquipment() { LoadEquipment(DataTableManager.MedicEquipment); }

        public void ButtonSniperEquipment() { LoadEquipment(DataTableManager.SniperEquipment); }

        public BindableCollection<Item> EquipmentDataTable
        {
            get { return _bc; }
            set
            {
                _bc = value;
                Console.WriteLine(@"NotifyOfPropertyChange(()=>WeaponDataTable)");
                NotifyOfPropertyChange(() => EquipmentDataTable);
            }
        }

    }
}