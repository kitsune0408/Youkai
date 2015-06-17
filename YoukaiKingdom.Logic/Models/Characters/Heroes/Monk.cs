namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    public class Monk : Hero
    {
        private const int DefaultHealth = 130;
        private const int DefaultMana = 200;
        private const int DefaultDamage = 150;
        private const int DefaultArmor = 50;

        public Monk(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public Monk(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

    }
}
