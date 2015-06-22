namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System.Timers;

    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class NpcWarrior : Npc
    {
        private Timer hitTimer;

        private const int DefaultAttackSpeed = 3000;

        public NpcWarrior(int level, string name, int health, int mana, int damage, int armor)
            : base(level, name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
        }

        public override void Hit(Interfaces.ICharacter target)
        {
            if (target is Hero && this.IsReadyToAttack)
            {
                var targetPlayer = (Hero)target;
                targetPlayer.ReceiveHit(this.Damage, AttackType.Physical);
                this.IsReadyToAttack = false;
                this.hitTimer.Start();
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
