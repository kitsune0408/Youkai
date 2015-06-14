namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Items.BonusAttributes;

    public abstract class Weapon : Item, IWeapon
    {
        protected Weapon(string name, int level, int attackPoints)
            : base(name, level)
        {
            this.AttackPoints = attackPoints;
        }

        public int AttackPoints { get; set; }

        public BonusAttributes Bonus { get; set; }
    }
}
