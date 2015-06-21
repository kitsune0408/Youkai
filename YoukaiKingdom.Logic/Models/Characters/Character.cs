namespace YoukaiKingdom.Logic.Models.Characters
{
    using YoukaiKingdom.Logic.Interfaces;

    public abstract class Character : ICharacter
    {
        protected Character(int level, string name, int maxHealth, int maxMana, int damage, int armor, int attackSpeed, int hitRange)
        {
            this.Name = name;
            this.MaxHealth = maxHealth;
            this.MaxMana = maxMana;
            this.Health = maxHealth;
            this.Mana = maxMana;
            this.Damage = damage;
            this.Armor = armor;
            this.Level = level;
            this.AttackSpeed = attackSpeed;
            this.HitRange = hitRange;
            this.IsReadyToAttack = true;
        }

        public string Name { get; set; }

        public int MaxHealth { get; set; }

        public int MaxMana { get; set; }

        public int Damage { get; set; }

        public int Armor { get; set; }

        public int Health { get; set; }

        public int Mana { get; set; }

        public int Level { get; set; }

        public int AttackSpeed { get; set; }

        public bool IsReadyToAttack { get; set; }

        public abstract void Hit(ICharacter target);

        public abstract void ReceiveHit(int damage, AttackType type);

        public int HitRange { get; set; }
    }
}
