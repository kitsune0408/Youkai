namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using System;

    /// <summary>
    /// Base class for non-player characters
    /// </summary>
    public abstract class Npc : Character
    {
        protected Npc(int level, string name, int health, int mana, int damage, int armor, int attackSpeed,int attackRange, Location location)
            : base(level, name, health, mana, damage, armor, attackSpeed, attackRange, location)
        { }

        public int DamageGotten { get; private set; }     
        public virtual void RemoveHealthPoints(int damage)
        {
            this.Health -= damage;
        }

        public override void ReceiveHit(int damage, AttackType type)
        {
            if (type == AttackType.Physical)
            {
                int dmg = Math.Max(0, damage - (this.Armor - (this.Level * 50)));
                this.DamageGotten = dmg;
                this.RemoveHealthPoints(dmg);
            }
            else if (type == AttackType.Magical)
            {
                this.RemoveHealthPoints(damage);
                this.DamageGotten = damage;
            }
        }
    }
}

