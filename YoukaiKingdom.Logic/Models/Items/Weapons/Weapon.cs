namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public abstract class Weapon : Item, IWeapon
    {
        protected Weapon(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level)
        {
            this.AttackPoints = attackPoints;
            this.AttackSpeed = attackSpeed;

            if (generateBonusAttributes)
            {
                this.Bonus = new BonusAttributes();
            }
        }

        public int AttackPoints { get; set; }

        public BonusAttributes Bonus { get; set; }

        public int AttackSpeed { get; set; }
    }
}
