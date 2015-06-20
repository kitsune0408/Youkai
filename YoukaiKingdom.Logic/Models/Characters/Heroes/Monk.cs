namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Characters.Spells;

    public class Monk : Hero
    {
        private const int DefaultHealth = 130;
        private const int DefaultMana = 400;
        private const int DefaultDamage = 150;
        private const int DefaultArmor = 50;

        private const int DefaultAttackSpeed = 1800;

        private readonly Fireball fireball;

        private Timer hitTimer;

        public Monk(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.fireball = Fireball.CreateFireball();
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
        }

        public Monk(string name)
            : this(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor)
        {
        }

        public override void Hit(ICharacter target)
        {
            if (target is Npc && this.IsReadyToAttack)
            {
                var targetNpc = (Npc)target;
                targetNpc.ReceiveHit(this.Damage, AttackType.Physical);
                this.IsReadyToAttack = false;
                this.hitTimer.Start();
            }
        }

        public void CastFireball(ICharacter enemy)
        {
            if (enemy is Npc && this.fireball.IsReady)
            {
                if (this.RemoveManaPointsAfterCast(this.fireball.ManaCost + (this.Level * 50)))
                {
                    enemy.ReceiveHit(this.fireball.Cast(this.Level), AttackType.Magical);
                    this.fireball.IsReady = false;
                }
            }
        }

        void HitTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReadyToAttack)
            {
                this.hitTimer.Stop();
            }

            this.IsReadyToAttack = true;
        }
    }
}
