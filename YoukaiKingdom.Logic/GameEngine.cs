namespace YoukaiKingdom.Logic
{
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    using YoukaiKingdom.Logic.Models.Items.Weapons;

    public class GameEngine
    {
        public GameEngine(Hero heroClass)
        {
            this.HeroClass = heroClass;
        }

        public Hero HeroClass { get; set; }

        public void Start()
        {
            this.HeroClass.Inventar.EquipMainHand(new OneHandedSword("TestSword", 1, 100));
            this.HeroClass.Inventar.EquipOffHand(new Shield("TestShield", 1, 150));
            this.HeroClass.Inventar.EquipArmor(new BodyArmor("BodyArmorTest", 1, 200));
            this.HeroClass.Inventar.AddItemToBag(new HealingPotion("healTest", 1, 50));
            this.HeroClass.Inventar.AddItemToBag(new HealingPotion("healTest", 1, 50));
            this.HeroClass.Inventar.RemoveItemFromBag(new HealingPotion("healTest", 1, 50));
        }
    }
}
