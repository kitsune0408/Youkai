namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    using YoukaiKingdom.Logic.Interfaces;

    public class OneHandedSword : Weapon, IOffhand
    {
        public OneHandedSword(string name, int level, int attackPoints) : base(name, level, attackPoints) { }
    }
}
