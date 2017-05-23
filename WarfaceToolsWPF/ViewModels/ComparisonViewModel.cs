using System;
using System.Collections.Generic;
using Caliburn.Micro;
using WarfaceToolsWPF.Models;

namespace WarfaceToolsWPF.ViewModels
{
    internal class ComparisonViewModel : PropertyChangedBase
    {
        private BindableCollection<Item> _bc1;
        private Item _selectedItem1;
        private BindableCollection<Item> _bc2;
        private Item _selectedItem2;

        private Tuple<string, string, string> _selectedClass1, _selectedClass2;

        public List<Tuple<string, string, string>> Classes { get; } = new List<Tuple<string, string, string>>()
        {
            Tuple.Create("Rifleman", DataTableManager.RiflemanWeapons, DataTableManager.RiflemanEquipment),
            Tuple.Create("Engineer", DataTableManager.EngineerWeapons, DataTableManager.EngineerEquipment),
            Tuple.Create("Medic", DataTableManager.MedicWeapons, DataTableManager.MedicEquipment),
            Tuple.Create("Sniper", DataTableManager.SniperWeapons, DataTableManager.SniperEquipment)
        };

        private bool _isWeaponSelected = true;

        public bool IsWeaponSelected
        {
            get { return _isWeaponSelected; }
            set
            {
                _isWeaponSelected = value;
                if (!string.IsNullOrEmpty(_selectedClass1?.Item1.ToString())) LoadList1(_selectedClass1);
                if (!string.IsNullOrEmpty(_selectedClass2?.Item1.ToString())) LoadList2(_selectedClass2);
                NotifyOfPropertyChange(() => IsWeaponSelected);
            }
        }

        public Tuple<string, string, string> SelectedClass1
        {
            get { return _selectedClass1; }
            set
            {
                _selectedClass1 = value;
                Console.WriteLine(@"NotifyOfPropertyChange(() => SelectedClass1)" + _selectedClass1);
                LoadList1(_selectedClass1);
                NotifyOfPropertyChange(() => SelectedClass1);
            }
        }

        public Tuple<string, string, string> SelectedClass2
        {
            get { return _selectedClass2; }
            set
            {
                _selectedClass2 = value;
                Console.WriteLine(@"NotifyOfPropertyChange(() => SelectedClass2)" + _selectedClass2);
                LoadList2(_selectedClass2);
                NotifyOfPropertyChange(() => SelectedClass2);
            }
        }

        private void LoadList1(Tuple<string,string,string> source)
        {
            ItemDataTable1 = IsWeaponSelected ? DataTableManager.LoadDataTable(source.Item2, true) : DataTableManager.LoadDataTable(source.Item3, false);
        }

        private void LoadList2(Tuple<string, string, string> source)
        {
            ItemDataTable2 = IsWeaponSelected ? DataTableManager.LoadDataTable(source.Item2, true) : DataTableManager.LoadDataTable(source.Item3, false);
        }

        #region Compared item 1
        public BindableCollection<Item> ItemDataTable1
        {
            get { return _bc1; }
            set
            {
                _bc1 = value;
                Console.WriteLine(@"NotifyOfPropertyChange(()=>WeaponDataTable)");
                NotifyOfPropertyChange(() => ItemDataTable1);
            }
        }
        
        public Item SelectedItem1
        {
            get { return _selectedItem1; }
            set
            {
                _selectedItem1 = value;
                if(_selectedItem1 != null) Console.WriteLine(@"NotifyOfPropertyChange(() => SelectedItem1)" + _selectedItem1.Name);
                NotifyOfPropertyChange(() => SelectedItem1);
            }
        }
        #endregion

        #region Compared item 2
        public BindableCollection<Item> ItemDataTable2
        {
            get { return _bc2; }
            set
            {
                _bc2 = value;
                Console.WriteLine(@"NotifyOfPropertyChange(()=>WeaponDataTable)");
                NotifyOfPropertyChange(() => ItemDataTable2);
            }
        }

        public Item SelectedItem2
        {
            get { return _selectedItem2; }
            set
            {
                _selectedItem2 = value;
                if(_selectedItem2 != null) Console.WriteLine(@"NotifyOfPropertyChange(() => SelectedItem2)" + _selectedItem2.Name);
                NotifyOfPropertyChange(() => SelectedItem2);
            }
        }
        #endregion

    }
}
