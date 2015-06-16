namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class Gloves : Armor
    {
        private const int DefaultDefense = 70;
        private const int DefaultLevel = 1;
        public Gloves(string name, int level, int defensePoints) : base(name, level, defensePoints) { }
        public Gloves(string name) : base(name, DefaultLevel, DefaultDefense) { }
       
    }
}
