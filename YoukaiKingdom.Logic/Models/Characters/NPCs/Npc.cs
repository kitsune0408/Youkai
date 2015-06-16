namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
     using YoukaiKingdom.Logic.Interfaces;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Items.Weapons;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    /// <summary>
    /// Base class for non-player characters
    /// </summary>
    public abstract class Npc : Character
    {
        protected Npc(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {}
            
        public virtual void RemoveHealthEffectsNPC(Hero damage)
        {
            this.Health -= damage.Damage;
        }
       
        
    }
}

