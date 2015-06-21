namespace YoukaiKingdom.Logic.Interfaces
{
    using YoukaiKingdom.Logic.Models.Characters;

    public interface ICharacter
    {
        string Name { get; set; }

        int MaxHealth { get; set; }

        int Health { get; set; }

        int MaxMana { get; set; }

        int Mana { get; set; }

        int Damage { get; set; }

        int Armor { get; set; }

        int Level { get; set; }

        int AttackSpeed { get; set; }

        int HitRange { get; set; }

        bool IsReadyToAttack { get; set; }

        void Hit(ICharacter target);

        void ReceiveHit(int damage, AttackType type);
    }
}
