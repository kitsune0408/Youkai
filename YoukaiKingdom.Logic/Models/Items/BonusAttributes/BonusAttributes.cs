namespace YoukaiKingdom.Logic.Models.Items.BonusAttributes
{
    using System;

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
            Random rand = new Random();
            int num = rand.Next(0, 100);
            if (num <= 20)
            {
                this.HasBonuses = false;
            }
            else
            {
                this.HasBonuses = true;
                
                if (num > 20 && num <= 50)
                {
                    this.АdditionalHealth = rand.Next(1, 20);
                }
                else if (num > 50 && num <= 75)
                {
                    this.АdditionalHealth = rand.Next(1, 20);
                    this.АdditionalMana = rand.Next(1, 20);
                }
                else if (num > 75 && num <= 90)
                {
                    this.АdditionalHealth = rand.Next(1, 20);
                    this.АdditionalMana = rand.Next(1, 20);
                    this.АdditionalArmor = rand.Next(1, 20);
                }
                else
                {
                    this.АdditionalHealth = rand.Next(1, 20);
                    this.АdditionalMana = rand.Next(1, 20);
                    this.АdditionalArmor = rand.Next(1, 20);
                    this.АdditionalDamage = rand.Next(1, 20);
                }
            }
        }
    }
}
