namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    /// <summary>
    /// Base class for non-player characters
    /// </summary>
    public abstract class Npc : Character
    {
        protected Npc(string name, int health, int mana, int damage, int armor)
            : base(name, health, mana, damage, armor)
        {
        }
    }
}
