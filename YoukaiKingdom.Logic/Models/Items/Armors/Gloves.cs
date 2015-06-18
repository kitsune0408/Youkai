namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    public class Gloves : Armor
    {
        private const int DefaultDefense = 70;
        private const int DefaultLevel = 1;

        public Gloves(int id, string name, int level, int defensePoints) : base(id, name, level, defensePoints) { }

        public Gloves(int id, string name) : base(id, name, DefaultLevel, DefaultDefense) { }
    }
}
