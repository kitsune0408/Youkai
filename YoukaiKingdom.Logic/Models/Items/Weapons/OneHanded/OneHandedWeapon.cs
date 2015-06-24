namespace YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class OneHandedWeapon : Weapon, IOffhand
    {
        protected OneHandedWeapon(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }
    }
}