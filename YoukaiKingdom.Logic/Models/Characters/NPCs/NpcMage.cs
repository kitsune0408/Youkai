namespace YoukaiKingdom.Logic.Models.Characters.NPCs
{
    using YoukaiKingdom.Logic.Interfaces;

    public class NpcMage : Npc
    {
        public NpcMage(string name, int health, int mana, int damage, int armor) : base(name, health, mana, damage, armor) { }
       
        public override void RemoveHealthEffectsNPC(Heroes.Hero damage)
        {
            base.RemoveHealthEffectsNPC(damage);
        }
        public override void Hit(ICharacter target)
        {
            throw new System.NotImplementedException();
        }
    }
}
