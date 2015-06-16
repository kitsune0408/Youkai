namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    public class Samurai : Hero
    {
        private const int DefaultHealth = 300;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 100;
        private const int DefaultArmor = 100;

        public Samurai(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Samurai(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }
    }
}
