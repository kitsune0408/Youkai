namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;

    public class OneHandedSword : Weapon, IOffhand
    {
        private const int DefaultAttackPoints = 70;
        private const int DefaultLevel = 1;

        public OneHandedSword(int id, string name, int level, int attackPoints) : base(id, name, level, attackPoints) { }

        public OneHandedSword(int id, string name) : base(id, name, DefaultLevel, DefaultAttackPoints) { }
    }
}
