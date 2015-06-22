namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Characters.Spells;

    public class NpcMage : Npc
    {
        private readonly Fireball fireball;

        private Timer hitTimer;

        private const int DefaultAttackSpeed = 5000;

        public NpcMage(int level, string name, int health, int mana, int damage, int armor)
            : base(level, name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.fireball = Fireball.CreateFireball(DefaultAttackSpeed);
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
        }

        public override void Hit(ICharacter target)
        {
            if (target is Hero && this.fireball.IsReady)
            {
                int npcDamage = this.fireball.Cast(this.Level) - 100;
                this.fireball.IsReady = false;
                target.ReceiveHit(npcDamage, AttackType.Magical);
            }
        }

        private void HitTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReadyToAttack)
            {
                this.hitTimer.Stop();
            }

            this.IsReadyToAttack = true;
        }
    }
}
