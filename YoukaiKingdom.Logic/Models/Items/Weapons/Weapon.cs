namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public abstract class Weapon : Item, IWeapon
    {
        protected Weapon(int id, string name, int level, int attackPoints, bool generateBonusAttributes = true)
            : base(id, name, level)
        {
            this.AttackPoints = attackPoints;

            if (generateBonusAttributes)
            {
                this.Bonus = new BonusAttributes();
            }
        }

        public int AttackPoints { get; set; }

        public BonusAttributes Bonus { get; set; }
    }
}
