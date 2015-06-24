namespace YoukaiKingdom.Logic.Models.Items.Weapons.OneHanded
{
    public abstract class OneHandedWeapon : Weapon
    {
        protected OneHandedWeapon(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }
    }
}