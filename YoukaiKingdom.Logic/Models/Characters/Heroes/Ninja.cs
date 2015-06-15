namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;

    public class Ninja : Hero
    {
        private const int DefaultHealth = 100;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 90;
        private const int DefaultArmor = 60;

        public Ninja(string name) : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Ninja(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }

        public override void Hit(ICharacter target)
        {
            throw new System.NotImplementedException();
        }
    }
}
