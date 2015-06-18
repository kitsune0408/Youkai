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
            this.HeroClass.ReplaceMainHand(new OneHandedSword(1, "TestSword", 1, 100));
            this.HeroClass.ReplaceOffHand(new Shield(2, "TestShield", 1, 150, false));
            this.HeroClass.ReplaceBodyArmor(new BodyArmor(3, "BodyArmorTest", 1, 200, false));
            this.HeroClass.Inventory.AddItemToBag(new HealingPotion(4, "healTest", 1, 50));
            this.HeroClass.Inventory.AddItemToBag(new HealingPotion(5, "healTest", 1, 50));
            var newItem = new OneHandedSword(6, "TestSword2", 1, 200, false);
            this.HeroClass.Inventory.AddItemToBag(newItem);
            this.HeroClass.Inventory.RemoveItemFromBag(new HealingPotion(5, "healTest", 1, 50));
            //this.HeroClass.AdjustEquipedItemStats();
            //var test = new NpcMage("testMageNPC", 500, 300, 200, 0);
            //this.HeroClass.Hit(test);
            //test.Hit(this.HeroClass);
            this.HeroClass.ReplaceMainHand(newItem);
        }
    }
}
