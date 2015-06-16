namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class Helmet : Armor
    {
        private const int DefaultDefense = 90;
        private const int DefaultLevel = 1;
        public Helmet(string name, int level, int defensePoints) : base(name, level, defensePoints) { }
        public Helmet(string name) : base(name, DefaultLevel, DefaultDefense) { }
       
    }
}
