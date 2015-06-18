namespace YoukaiKingdom.Logic.Models.Items.Armors
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public abstract class Armor : Item, IArmor
    {
        protected Armor(int id, string name, int level, int defensePoints, bool generateBonusAttributes = true)
            : base(id, name, level)
        {
            this.DefensePoints = defensePoints;
            if (generateBonusAttributes)
            {
                this.Bonus = new BonusAttributes();
            }
        }

        public int DefensePoints { get; set; }

        public BonusAttributes Bonus { get; set; }
    }
}
