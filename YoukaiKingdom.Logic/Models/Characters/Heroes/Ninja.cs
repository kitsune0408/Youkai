namespace YoukaiKingdom.Logic.Models.Characters.Heroes
{
    using System.Timers;

    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Characters.Spells;

    public class Ninja : Hero
    {
        private const int DefaultHealth = 200;
        private const int DefaultMana = 50;
        private const int DefaultDamage = 70;
        private const int DefaultArmor = 50;
        private readonly ProtectingShadow protectedOfDamage;
        private const int DefaultAttackSpeed = 2000;
        private Timer hitTimer;
        
        public Ninja(string name) : this(name, DefaultHealth, DefaultMana, DefaultDamage, DefaultArmor) { }

        public Ninja(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor, DefaultAttackSpeed)
        {
            this.hitTimer = new Timer(this.AttackSpeed);
            this.hitTimer.Elapsed += this.HitTimerElapsed;
            this.protectedOfDamage = ProtectingShadow.CreateProtectedOfDamage();
        }

        #region Default Values

        public static int DefaultNinjaHealth
        {
            get
            {
                return DefaultHealth;
            }
        }

        public static int DefaultNinjaArmor
        {
            get
            {
                return DefaultArmor;
            }
        }

        public static int DefaultNinjaDamage
        {
            get
            {
                return DefaultDamage;
            }
        }

        public static int DefaultNinjaMana
        {
            get
            {
                return DefaultMana;
            }
        }
        public static int DefaultNinjaAttackSpeed
        {
            get
            {
                return DefaultAttackSpeed;
            }
        }
        public bool ProtectedOfDamageIsReady
        {
            get
            {
                return this.protectedOfDamage.IsReady;
            }
        }
        public int ProtectedOfDamageCastRange
        {
            get
            {
                return this.protectedOfDamage.SpellRange;
            }
        }
        #endregion Default Values

        public override void Hit(ICharacter target)
        {
            if (target is Npc && this.IsReadyToAttack)
            {
                var targetNpc = (Npc)target;
                targetNpc.ReceiveHit(this.Damage, AttackType.Physical);
                this.hitTimer.Interval = this.AttackSpeed;
                this.IsReadyToAttack = false;
                this.hitTimer.Start();
            }
        }

        public bool CastProtectedOfDamage()
        {
            if (this.protectedOfDamage.IsReady)
            {
                if (this.RemoveManaPointsAfterCast(this.protectedOfDamage.ManaCost))
                {
                    this.protectedOfDamage.Cast();
                    return true;
                }
            }

            return false;
        }

        private void HitTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.IsReadyToAttack)
            {
                this.hitTimer.Stop();
            }

            this.IsReadyToAttack = true;
        }

        public override void ReceiveHit(int damage, AttackType type)
        {
            if (!this.protectedOfDamage.IsProtecting)
            {
                base.ReceiveHit(damage, type);
            }
        }
    }
}
