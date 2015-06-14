namespace YoukaiKingdom.Logic.Models.Items.BonusAttributes
{
    using System;

    using YoukaiKingdom.Logic.Interfaces;

    public class BonusAttributes : IBonusAttributes
    {
        public bool HasBonuses { get; set; }

        public int АdditionalHealth { get; set; }

        public int АdditionalMana { get; set; }

        public int АdditionalDamage { get; set; }

        public int АdditionalArmor { get; set; }

        public void GenerateBonusAttributes()
        {
            Random rand = new Random();
            int num = rand.Next(0, 100);
            if (num <= 20)
            {
                this.HasBonuses = false;
            }

            //TODO
        }
    }
}
