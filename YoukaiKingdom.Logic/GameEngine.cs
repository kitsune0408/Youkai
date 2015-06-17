namespace YoukaiKingdom.Logic
{
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
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
            this.HeroClass.Inventory.EquipMainHand(new OneHandedSword("TestSword", 1, 100));
            this.HeroClass.Inventory.EquipOffHand(new Shield("TestShield", 1, 150));
            this.HeroClass.Inventory.EquipArmor(new BodyArmor("BodyArmorTest", 1, 200));
            this.HeroClass.Inventory.AddItemToBag(new HealingPotion("healTest", 1, 50));
            this.HeroClass.Inventory.AddItemToBag(new HealingPotion("healTest", 1, 50));
            this.HeroClass.Inventory.RemoveItemFromBag(new HealingPotion("healTest", 1, 50));
            this.HeroClass.AdjustEquipedItemStats();
            var test = new NpcMage("testMageNPC", 500, 300, 200, 0);
            this.HeroClass.Hit(test);
            test.Hit(this.HeroClass);
        }
    }
}
