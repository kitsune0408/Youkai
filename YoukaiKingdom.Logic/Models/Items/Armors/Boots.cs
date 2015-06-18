namespace YoukaiKingdom.Logic.Models.Items.Armors
{

    public class Boots : Armor
    {
        private const int DefaultDefense = 30;
        private const int DefaultLevel = 1;

        public Boots(int id, string name, int level, int defensePoints) : base(id, name, level, defensePoints) { }

        public Boots(int id, string name) : base(id, name, DefaultLevel, DefaultDefense) { }
    }
}
