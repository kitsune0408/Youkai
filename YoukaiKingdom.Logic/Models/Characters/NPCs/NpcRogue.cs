namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System.Timers;

    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    public class NpcRogue : Npc
    {
        private const int DefaultHealth = 180;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 90;
        private const int DefaultArmor = 70;

        private Timer hitTimer;

        private const int DefaultAttackSpeed = 2000;

        public NpcRogue(int level, string name, Location location) : this(level, name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor, location) { }

        public NpcRogue(int level, string name, int health, int mana, int damage, int armor, Location location)
            : base(level, name, health, mana, damage, armor, DefaultAttackSpeed, location)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
            this.MaxHealth += (this.Level * 60);
            this.MaxMana += (this.Level * 10);
            this.Armor += (this.Level * 30);
            this.Damage += (this.Level * 25);
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
