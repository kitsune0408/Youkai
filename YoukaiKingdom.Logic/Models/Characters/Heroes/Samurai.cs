namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;

    public class Samurai : Hero
    {
        private const int DefaultHealth = 300;
        private const int DefaultMana = 30;
        private const int DefaultDamage = 100;
        private const int DefaultArmor = 100;
        private const int DefaultAttackSpeed = 100;

        private Timer hitTimer;

        public Samurai(string name) : this(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Samurai(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
        }

        #region Default Values

        public static int DefaultSamuraiHealth
        {
            get
            {
                return DefaultHealth;
            }
        }

        public static int DefaultSamuraiArmor
        {
            get
            {
                return DefaultArmor;
            }
        }

        public static int DefaultSamuraiDamage
        {
            get
            {
                return DefaultDamage;
            }
        }

        public static int DefaultSamuraiMana
        {
            get
            {
                return DefaultMana;
            }
        }

        #endregion Default Values

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
