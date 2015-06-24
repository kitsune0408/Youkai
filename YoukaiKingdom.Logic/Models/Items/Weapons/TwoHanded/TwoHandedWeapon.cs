namespace YoukaiKingdom.Logic.Models.Items.Weapons
{
    public abstract class TwoHandedWeapon : Weapon
    {
        protected TwoHandedWeapon(int id, string name, int level, int attackPoints, int attackSpeed, bool generateBonusAttributes = true)
            : base(id, name, level, attackPoints, attackSpeed, generateBonusAttributes) { }
    }
}
