namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;

    public class OneHandedSword : Weapon, IOffhand
    {
        private const int DefaultAttackPoints = 70;
        private const int DefaultLevel = 1;

        public OneHandedSword(string name, int level, int attackPoints) : base(name, level, attackPoints) { }
        public OneHandedSword(string name) : base(name, DefaultLevel, DefaultAttackPoints) { }
       
    }
}
