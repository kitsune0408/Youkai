namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    public class Monk : Hero
    {
        private const int DefaultHealth = 150;
        private const int DefaultMana = 120;
        private const int DefaultDamage = 70;
        private const int DefaultArmor = 90;

        public Monk(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public Monk(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

    }
}
