namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    using YoukaiKingdom.Logic.Interfaces;

    public class Shield : Armor, IOffhand
    {
        private const int DefaultDefense = 100;
        private const int DefaultLevel = 1;

        public Shield(int id, string name, int level, int defensePoints) : base(id, name, level, defensePoints) { }

        public Shield(int id, string name) : base(id, name, DefaultLevel, DefaultDefense) { }
    }
}
