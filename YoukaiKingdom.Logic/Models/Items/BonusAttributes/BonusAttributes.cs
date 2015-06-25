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
                this.АdditionalHealth = rand.Next(0, 10); //this.GenerateStats();
                this.АdditionalArmor = rand.Next(0, 10); //this.GenerateStats();
                this.АdditionalDamage = rand.Next(0, 10); //this.GenerateStats();
                this.АdditionalMana = rand.Next(0, 10);//this.GenerateStats();
            }
        }

        //private int GenerateStats()
        //{
        //    Random rand = new Random();
        //    int num = rand.Next(0, 100);

        //    if (num <= 20)
        //    {
        //        return 0;
        //    }

        //    return rand.Next(0, 10);
        //}
    }
}
