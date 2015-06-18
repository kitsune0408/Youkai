namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public abstract class Armor : Item, IArmor
    {
        protected Armor(int id, string name, int level, int defensePoints)
            : base(id, name, level)
        {
            this.DefensePoints = defensePoints;
        }

        public int DefensePoints { get; set; }

        public BonusAttributes Bonus { get; set; }
    }
}
