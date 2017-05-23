namespace WarfaceToolsWPF.Models
{
    internal class Item
    {
        // shared
        public string Name { set; get; }
        public byte[] Picture { set; get; }
        public int Cost { set; get; }
        public string Obtainment { set; get; }
        public int IsEsl { set; get; }
        public int IsCup { set; get; }

        // weapon
        public int Damage { set; get; }
        public int Range { set; get; }
        public int RateOfFire { set; get; }
        public int AimAccuracy { set; get; }
        public int HipAccuracy { set; get; }
        public int AmmunitionCapacity { set; get; }

        // equipment
        public string Perk1 { set; get; }
        public string Perk2 { set; get; }
        public string Perk3 { set; get; }
        public string Perk4 { set; get; }
        
    }
}
