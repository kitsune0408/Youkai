namespace YoukaiKingdom.Logic.Models.Items.BonusAttributes
{
    using System.Linq;

    using YoukaiKingdom.Logic.Helpers;
    using YoukaiKingdom.Logic.Interfaces;

    public class BonusAttributes : IBonusAttributes
    {
        public BonusAttributes()
        {
            this.GenerateBonusAttributes();
        }

        public bool HasBonuses { get; private set; }

        public int АdditionalHealth { get; private set; }

        public int АdditionalMana { get; private set; }

        public int АdditionalDamage { get; private set; }

        public int АdditionalArmor { get; private set; }

        private void GenerateBonusAttributes()
        {
            int num = Utility.GetRandom(0, 100);
            if (num <= 20)
            {
                this.HasBonuses = false;
            }
            else
            {
                this.HasBonuses = true;

                if (num > 20 && num <= 50)
                {
                    this.GenerateRandomOrder(1);
                }
                else if (num > 50 && num <= 75)
                {
                    this.GenerateRandomOrder(2);
                }
                else if (num > 75 && num <= 90)
                {
                    this.GenerateRandomOrder(3);
                }
                else
                {
                    this.GenerateRandomOrder(4);
                }
            }
        }

        private void GenerateRandomOrder(int attributeCount)
        {
            var randomNumbers = Enumerable.Range(0, 4)
                          .Select(x => new { val = x, order = Utility.GetRandom(0, 100) })
                          .OrderBy(i => i.order)
                          .Select(x => x.val)
                          .ToArray();

            for (int i = 0; i < attributeCount; i++)
            {
                this.AddStats((RandomAttributeTypes)randomNumbers[i]);
            }
        }

        private void AddStats(RandomAttributeTypes type)
        {
            if (type == RandomAttributeTypes.Health)
            {
                this.АdditionalHealth = Utility.GetRandom(1, 20);
            }
            else if (type == RandomAttributeTypes.Mana)
            {
                this.АdditionalMana = Utility.GetRandom(1, 20);
            }
            else if (type == RandomAttributeTypes.Armor)
            {
                this.АdditionalArmor = Utility.GetRandom(1, 20);
            }
            else if (type == RandomAttributeTypes.Damage)
            {
                this.АdditionalDamage = Utility.GetRandom(1, 20);
            }
        }
    }

    public enum RandomAttributeTypes
    {
        Health = 0, Mana = 1, Armor = 2, Damage = 3
    }
}
