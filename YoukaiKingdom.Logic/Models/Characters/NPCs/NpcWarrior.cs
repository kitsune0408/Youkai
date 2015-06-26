namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class NpcWarrior : Npc
    {
        private const int DefaultHealth = 250;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 80;
        private const int DefaultArmor = 50;

        private Timer hitTimer;

        private const int DefaultAttackSpeed = 3000;

        private const int DefaultHitRange = 1;

        public NpcWarrior(int level, string name, Location location) : this(level, name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor, location) { }

        public NpcWarrior(int level, string name, int health, int mana, int damage, int armor, Location location)
            : base(level, name, health, mana, damage, armor, DefaultAttackSpeed, DefaultHitRange, location)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
            this.MaxHealth += (this.Level * 70);
            this.MaxMana += (this.Level * 10);
            this.Armor += (this.Level * 70);
            this.Damage += (this.Level * 30);
            //added this so enemies don't hang around with partly depleted health
            this.Health = this.MaxHealth;
            this.Mana = this.MaxMana;
        }

        public override void Hit(ICharacter target)
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
