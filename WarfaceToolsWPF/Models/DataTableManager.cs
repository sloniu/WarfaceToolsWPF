using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Caliburn.Micro;
using MySql.Data.MySqlClient;

namespace WarfaceToolsWPF.Models
{
    internal class DataTableManager
    {
        public static string RiflemanWeapons { get; } = "rifleman_weapons";
        public static string EngineerWeapons { get; } = "engineer_weapons";
        public static string MedicWeapons { get; } = "medic_weapons";
        public static string SniperWeapons { get; } = "sniper_weapons";
        public static string Pistols { get; } = "pistol_weapons";

        public static string RiflemanEquipment { get; } = "rifleman_equipment";
        public static string EngineerEquipment { get; } = "engineer_equipment";
        public static string MedicEquipment { get; } = "medic_equipment";
        public static string SniperEquipment { get; } = "sniper_equipment";

        private List<string> _dataTableNotFoundList;

        private readonly List<string> _dataTableNames = new List<string>(
            new[] { RiflemanWeapons, EngineerWeapons, MedicWeapons, SniperWeapons, Pistols, RiflemanEquipment, EngineerEquipment, MedicEquipment, SniperEquipment });
       
        /// <summary>
        /// Saves datatable from server to file
        /// </summary>
        /// <param name="tableName">datatable name</param>
        private void SaveDataTable(string tableName)
        {
            if (Login.Conn.State.ToString() != "Open")
            {
                Login.Conn.Open();
                Console.WriteLine(@"Connection opened from SaveDataTable.");
            }
            string command = "select *  from " + tableName;
            Console.WriteLine(@"command: " + command);
            Console.WriteLine(@"conn: " + Login.Conn);

            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command, Login.Conn);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tableName);
            DataTable dataTable = dataSet.Tables[0];
            
            FileStream dataTableFileStream = new FileStream(tableName + ".bin", FileMode.Create);
            BinaryFormatter dataTableBinaryFormatter = new BinaryFormatter();
            dataTableBinaryFormatter.Serialize(dataTableFileStream, dataTable);
            dataTableFileStream.Close();
        }

        /// <summary>
        /// Loads datatable from file
        /// </summary>
        /// <param name="tableName">datatable name</param>
        /// <param name="isWeapon">true if weaponsDB, false if equipmentsDB</param>
        /// <returns>BindableCollection of Item type objects</returns>
        public static BindableCollection<Item> LoadDataTable(string tableName, bool isWeapon)
        {
            FileStream dataTableFileStream = new FileStream(tableName + ".bin", FileMode.Open);
            BinaryFormatter dataTableBinaryFormatter = new BinaryFormatter();
            DataTable dataTable = (DataTable)dataTableBinaryFormatter.Deserialize(dataTableFileStream);
            dataTableFileStream.Close();

            BindableCollection<Item> collection = new BindableCollection<Item>();
            if (isWeapon)
                foreach (DataRow row in dataTable.Rows)
                {
                    byte[] picture;
                    if (row.ItemArray[0].GetType().ToString() != "System.DBNull")
                        picture = (byte[])row.ItemArray[0];
                    else
                        picture = new byte[0];
                    var item = new Item
                    {
                        Picture = picture,
                        Name = row.ItemArray[1] as string,
                        Damage = (int)row.ItemArray[2],
                        Range = (int)row.ItemArray[3],
                        RateOfFire = (int)row.ItemArray[4],
                        AimAccuracy = (int)row.ItemArray[5],
                        HipAccuracy = (int)row.ItemArray[6],
                        AmmunitionCapacity = (int)row.ItemArray[7],
                        Cost = (int)row.ItemArray[8],
                        Obtainment = (string)row.ItemArray[9],
                        IsEsl = (int)row.ItemArray[10]
                    };
                    collection.Add(item);
                }
            else
                foreach (DataRow row in dataTable.Rows)
                {
                    byte[] picture;
                    if (row.ItemArray[0].GetType().ToString() != "System.DBNull")
                        picture = (byte[])row.ItemArray[0];
                    else
                        picture = new byte[0];

                    var item = new Item
                    {
                        Picture = picture,
                        Name = (string)row.ItemArray[1],
                        Perk1 = (string)row.ItemArray[2],
                        Perk2 = (string)row.ItemArray[3],
                        Perk3 = (string)row.ItemArray[4],
                        Perk4 = (string)row.ItemArray[5],
                        Cost = (int)row.ItemArray[6],
                        IsEsl = (int)row.ItemArray[7],
                        IsCup = (int)row.ItemArray[8]
                    };
                    collection.Add(item);
                }
            return collection;
        }

        public bool CheckIfDataTablesExist()
        {
            _dataTableNotFoundList = _dataTableNames.Where(s => !File.Exists(s + ".bin")).ToList();
            return !_dataTableNotFoundList.Any();
        }

        public void ReDownloadDataTables()
        {
            foreach (string s in _dataTableNotFoundList)
            {
                Console.WriteLine(@"downloading from dataTableNotFoundList: " + s);
                SaveDataTable(s);
            }
        }
    }
}
