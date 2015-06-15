namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    using YoukaiKingdom.Logic.Interfaces;

    public class Shield : Armor, IOffhand
    {
        public Shield(string name, int level, int defensePoints) : base(name, level, defensePoints) { }
    }
}
