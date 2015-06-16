namespace YoukaiKingdom.Logic.Models.Items.Armors
{

    public class Boots : Armor
    {
        private const int DefaultDefense = 30;
        private const int DefaultLevel = 1;
        public Boots(string name, int level, int defensePoints) : base(name, level, defensePoints) { }
        public Boots(string name) : base(name, DefaultLevel, DefaultDefense) { }
        
    }
}
