namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    using YoukaiKingdom.Logic.Interfaces;

    public class Shield : Armor, IOffhand
    {
        private const int DefaultDefense = 100;
        private const int DefaultLevel = 1;
        public Shield(string name, int level, int defensePoints) : base(name, level, defensePoints) { }
        public Shield(string name) : base(name, DefaultLevel, DefaultDefense) { }
       
    }
}
