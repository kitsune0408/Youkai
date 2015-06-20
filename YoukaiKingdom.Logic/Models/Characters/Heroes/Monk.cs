namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.Magics;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;

    public class Monk : Hero
    {
        private const int DefaultHealth = 130;
        private const int DefaultMana = 200;
        private const int DefaultDamage = 150;
        private const int DefaultArmor = 50;

        private readonly Fireball fireball;

        public Monk(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {
            this.fireball = Fireball.CreateFireball();
        }

        public Monk(string name)
            : base(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor)
        {
            this.fireball = Fireball.CreateFireball();
        }

        public void CastFireball(ICharacter enemy)
        {
            if (enemy is Npc)
            {
                enemy.ReceiveHit(this.fireball.Cast(this.Level), AttackType.Magical);
                this.RemoveManaPointsAfterCast(this.fireball.ManaCost + (this.Level * 50));
            }
        }

    }
}
