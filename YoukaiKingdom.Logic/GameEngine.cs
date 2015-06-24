namespace YoukaiKingdom.Logic
{
    using System.Collections.Generic;

    using YoukaiKingdom.Logic.Models.Characters;
    using YoukaiKingdom.Logic.Models.Characters.Heroes;
    using YoukaiKingdom.Logic.Models.Characters.NPCs;
    using YoukaiKingdom.Logic.Models.Items.Armors;
    using YoukaiKingdom.Logic.Models.Items.Potions;
    using YoukaiKingdom.Logic.Models.Items.Weapons;

    public class GameEngine
    {
        
        public GameEngine(Hero heroClass)
        {
            this.Hero = heroClass;
            this.LoadDefaultInfo();
            this.Enemies = new List<Npc>();
            this.LoadBosses();
            this.CurrentLevel = 1;
        }

        public Hero Hero { get; set; }

        public void Start()
        {
            this.LoadEnemiesByLevel(this.CurrentLevel);
        }

        public List<Npc> Enemies { get; set; }

        public List<Npc> Bosses { get; set; }

        public int CurrentLevel { get; set; }

        public void LoadEnemiesByLevel(int level)
        {
            this.Enemies.AddRange(new List<Npc>()
            {
                new NpcRogue(level, "Evil Ninja", new Location(1200, 300, 200, 200, 150)),//0
                new NpcRogue(level, "Evil Ninja", new Location(1200, 800, 200, 200, 100)),//1
                new NpcRogue(level, "Evil Ninja", new Location(400, 900, 200, 200, 100)),//2
                new NpcRogue(level, "Evil Ninja", new Location(2200, 0, 200, 200, 100)),//3
                new NpcRogue(level, "Evil Ninja", new Location(2800, 800, 200, 200, 100)),//4
                new NpcRogue(level, "Evil Ninja", new Location(1200, 2000, 200, 200, 100)),//5
                new NpcMage(level, "Evil Mage", new Location(100, 900, 300, 300, 100)),//6
                new NpcMage(level, "Evil Mage", new Location(1000, 1400, 200, 200, 100)),//7
                new NpcMage(level, "Evil Mage", new Location(2000, 1200, 400, 400, 100)),//8
                new NpcMage(level, "Evil Mage", new Location(3200, 0, 200, 200, 100)),//9   
                new NpcMage(level, "Evil Mage", new Location(1000, 2200, 200, 200, 100)),//10  
                new NpcWarrior(level, "Evil Samurai", new Location(1000, 1100, 200, 200, 100)),//11
                new NpcWarrior(level, "Evil Samurai", new Location(600, 1100, 200, 200, 100)),//12
                new NpcWarrior(level, "Evil Samurai", new Location(2000, 800, 200, 200, 100)),//13
                new NpcWarrior(level, "Evil Samurai", new Location(2800, 1200, 200, 200, 100)),//14
                new NpcWarrior(level, "Evil Samurai", new Location(3000, 0, 200, 200, 100)),//15
                new NpcWarrior(level, "Evil Samurai", new Location(1400, 1800, 200, 200, 100)),//15
                //this.bosses[level-1]
            });
        }

        public void LoadBosses()
        {
            this.Bosses = new List<Npc>();

            this.Bosses.AddRange(new List<Npc>()
                                {
                                    new NpcWarrior(1, "Oni", 600, 100, 250, 200, new Location(3200, 2200, 400, 200, 100)),
                                    new NpcMage(2, "Ogre", 800, 600, 250, 300, new Location(100 ,100, 100, 100, 100)),
                                    new NpcRogue(3, "Goryo", 850, 100, 300, 300, new Location(100 ,100, 100, 100, 100))
                                });
        }

        private void LoadDefaultInfo()
        {
            if (this.Hero is Samurai)
            {
                this.Hero.ReplaceMainHand(new OneHandedSword(1, "Iron sword", 1500, false));
                this.Hero.ReplaceBodyArmor(new BodyArmor(2, "Iron armor", false));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(3, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new Gloves(4, "Iron gloves"));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(5, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(6, "Minor healing potion", 1, 20));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(7, "Minor healing potion", 1, 20));
                this.Hero.Inventory.AddItemToBag(new Gloves(8, "Steel gloves", 1, 15, false));
                this.Hero.Inventory.AddItemToBag(new ManaPotion(9, "Mana potion", 1, 50));
            }
            else if (this.Hero is Monk)
            {
                this.Hero.ReplaceMainHand(new TwoHandedStaff(1, "Staff", 3200, false));
                this.Hero.ReplaceBodyArmor(new BodyArmor(2, "Woolen robe", false));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(3, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new Gloves(4, "Woolen gloves"));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(5, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(6, "Minor healing potion", 1, 20));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(7, "Minor healing potion", 1, 20));
                this.Hero.Inventory.AddItemToBag(new ManaPotion(9, "Mana potion", 1, 50));
            }
            else if (this.Hero is Ninja)
            {
                this.Hero.ReplaceMainHand(new OneHandedDagger(1, "Rusted Dagger", 1400, false));
                this.Hero.ReplaceBodyArmor(new BodyArmor(2, "Leather jacket", false));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(3, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new Gloves(4, "Leather gloves"));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(5, "Healing potion", 1, 50));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(6, "Minor healing potion", 1, 20));
                this.Hero.Inventory.AddItemToBag(new HealingPotion(7, "Minor healing potion", 1, 20));
                this.Hero.Inventory.AddItemToBag(new ManaPotion(9, "Mana potion", 1, 50));
            }
        }
    }
}
