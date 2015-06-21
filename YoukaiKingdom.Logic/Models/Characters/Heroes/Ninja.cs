namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;

    public class Ninja : Hero
    {
        private const int DefaultHealth = 150;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 100;
        private const int DefaultArmor = 60;

        private const int DefaultSpeed = 1400;
        private Timer hitTimer;

        public Ninja(string name) : this(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Ninja(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor, DefaultSpeed)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
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
