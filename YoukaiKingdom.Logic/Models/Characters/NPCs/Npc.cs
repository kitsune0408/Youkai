namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;

    /// <summary>
    /// Base class for non-player characters
    /// </summary>
    public abstract class Npc : Character
    {
        protected Npc(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        { }

        public virtual void RemoveHealthPoints(Hero damage)
        {
            this.Health -= damage.Damage;
        }

        public override void Hit(ICharacter target)
        {
            if (target is Hero)
            {
                var targetPlayer = (Hero)target;
                targetPlayer.ReceiveHit(this);
            }
        }

        public override void ReceiveHit(ICharacter enemy)
        {
            if (enemy is Hero)
            {
                this.Health -= enemy.Damage;
            }
        }
    }
}

