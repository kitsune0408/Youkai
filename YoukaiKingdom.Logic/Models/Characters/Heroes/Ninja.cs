namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    public class Ninja : Hero
    {
        private const int DefaultHealth = 150;
        private const int DefaultMana = 0;
        private const int DefaultDamage = 100;
        private const int DefaultArmor = 60;

        public Ninja(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Ninja(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

    }
}
